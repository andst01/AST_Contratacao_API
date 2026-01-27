using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces.Service;
using Contratacao.Application.Service;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Enums;
using Contratacao.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace Contratacao.Application.Test
{
    public class ApoliceServiceTest
    {
        private IFixture Fixture;
        private Mock<IApoliceRepoitorio> _mockApoliceRepoitorio;
        private Mock<IRepositorioBase<Proposta>> _mockPropostaRepositorio;
        private Mock<IMapper> _mockMapper;
        private IApoliceService _service;

        public ApoliceServiceTest()
        {
            Fixture = new Fixture()
                .Customize(new AutoMoqCustomization
                {
                    ConfigureMembers = true
                });
            _mockApoliceRepoitorio = new Mock<IApoliceRepoitorio>();
            _mockPropostaRepositorio = new Mock<IRepositorioBase<Proposta>>();
            _mockMapper = new Mock<IMapper>();
            _service = new ApoliceService(_mockApoliceRepoitorio.Object, _mockPropostaRepositorio.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Nao_Deve_Criar_Apolice_Com_Proposta_Nao_Aprovada()
        {
            var proposta = new Proposta();
            proposta.Status = EnumStatusProposta.EmAnalise;

            var apolice = Fixture.Build<Apolice>()
                .Without(x => x.Proposta)
                .Create();

            _mockMapper
                .Setup(m => m.Map<Apolice>(It.IsAny<ApoliceDTO>()))
                .Returns(apolice);

            _mockPropostaRepositorio
                .Setup(r => r.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(proposta);

            Assert.ThrowsAsync<Exception>(() =>
                _service.CriarApoliceAsync(new ApoliceDTO { IdProposta = 1 })
            );
        }

        [Test]
        public async Task Deve_Criar_Apolice_Com_Proposta_Aprovada()
        {
            var proposta = Fixture.Build<Proposta>()
                .Without(x => x.Apolice)
                .Without(x => x.Cliente)
                .Create();

            proposta.Id = 10;
            proposta.Status = EnumStatusProposta.Aprovada;

            var apolice = Fixture.Build<Apolice>()
               .Without(x => x.Proposta)
               .Create();

            apolice.IdProposta = 10;
            apolice.Status = EnumStatusApolice.Ativa;

            _mockMapper
                .Setup(m => m.Map<Apolice>(It.IsAny<ApoliceDTO>()))
                .Returns(apolice);

            _mockPropostaRepositorio
                .Setup(r => r.ObterPorIdAsync(proposta.Id))
                .ReturnsAsync(proposta);



            var apoliceDTO = Fixture.Create<ApoliceDTO>();
            apoliceDTO.IdProposta = 10;
            apoliceDTO.CodigoStatus = (int)EnumStatusApolice.Ativa;

            _mockMapper.Setup(x => x.Map<ApoliceDTO>(It.IsAny<Apolice>()))
                .Returns(apoliceDTO);

            _mockApoliceRepoitorio
                .Setup(r => r.AdicionarAsync(It.IsAny<Apolice>()))
                .ReturnsAsync(apolice);

            _mockApoliceRepoitorio.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var retorno = await _service.CriarApoliceAsync(apoliceDTO);
           
            Assert.NotNull(retorno);
           
        }
    }
}
