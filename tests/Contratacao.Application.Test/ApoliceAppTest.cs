using AutoFixture;
using AutoMapper;
using Bogus;
using Contratacao.Application.DTO;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Enums;
using Contratacao.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace Contratacao.Application.Test
{
    public class ApoliceAppTests : AppBaseTest<ApoliceApp>
    {
        private Mock<IApoliceRepoitorio> _repositorioMock = null!;
        private Mock<IMapper> _mapperMock = null!;
        private ApoliceApp _app = null!;


        [SetUp]
        public void Setup()
        {
            _repositorioMock = FreezeMock<IApoliceRepoitorio>();
            _mapperMock = FreezeMock<IMapper>();

            _app = CreateSut();
        }

        [Test]
        public async Task AdicionarAsync_DeveAdicionarERetornarViewModel()
        {
            var dto = Fixture.Create<ApoliceDTO>();

            var entity = Fixture.Build<Apolice>()
                        .Without(p => p.Proposta).Create();

            _mapperMock
                .Setup(m => m.Map<Apolice>(dto))
                .Returns(entity);

            _repositorioMock
                .Setup(r => r.AdicionarAsync(entity))
                .ReturnsAsync(entity);

            _mapperMock
                .Setup(m => m.Map<ApoliceDTO>(entity))
                .Returns(dto);

            var result = await _app.AdicionarAsync(dto);

            Assert.NotNull(result);
            _repositorioMock.Verify(r => r.AdicionarAsync(entity), Times.Once);
        }

        [Test]
        public async Task AtualizarAsync_ComId_DeveAtualizar()
        {
            var dto = Fixture.Create<ApoliceDTO>();
            var entity = Fixture.Build<Apolice>()
                        .Without(p => p.Proposta).Create();
            var id = Fixture.Create<int>();

            _mapperMock.Setup(m => m.Map<Apolice>(dto))
                       .Returns(entity);

            _repositorioMock.Setup(r => r.AtualizarAsync(entity, id))
                            .ReturnsAsync(entity);

            _mapperMock.Setup(m => m.Map<ApoliceDTO>(entity))
                       .Returns(dto);

            var result = await _app.AtualizarAsync(dto, id);

            Assert.NotNull(result);
            _repositorioMock.Verify(r => r.AtualizarAsync(entity, id), Times.Once);
        }

        [Test]
        public async Task ExcluirAsync_DeveChamarRepositorio()
        {
            var id = Fixture.Create<int>();

            _repositorioMock
                .Setup(r => r.ExcluirAsync(id))
                .ReturnsAsync(1);

            var result = await _app.ExcluirAsync(id);

            Assert.AreEqual(1, result);
            _repositorioMock.Verify(r => r.ExcluirAsync(id), Times.Once);
        }

        [Test]
        public async Task ObterTodosAsync_DeveRetornarLista()
        {
            var entities = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .CreateMany<Apolice>(3).ToList();
            var dto = Fixture.CreateMany<ApoliceDTO>(3).ToList();

            _repositorioMock.Setup(r => r.ObterTodosAsync())
                            .ReturnsAsync(entities);

            _mapperMock.Setup(m => m.Map<List<ApoliceDTO>>(entities))
                       .Returns(dto);

            var result = await _app.ObterTodosAsync();

            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public async Task ObterDadosContratacaoClienteAsync_DeveRetornarLista()
        {
          
            var entities = new Faker<Apolice>()
                        .RuleFor(a => a.Id, f => f.Random.Int(1, 1000))
                        .RuleFor(a => a.NumeroApolice, f => f.Finance.Account())
                        .RuleFor(a => a.Status, f => f.PickRandom<EnumStatusApolice>())
                        .RuleFor(a => a.DataInicioVigencia, f => f.Date.Past())
                        .RuleFor(a => a.DataFimVigencia, f => f.Date.Future())
                        .RuleFor(a => a.PremioFinal, f => f.Finance.Amount())
                        .RuleFor(a => a.IdProposta, f => f.Random.Int(1, 1000))
                        .RuleSet("WithProposta", rules =>
                        {
                            rules.RuleFor(a => a.Proposta, f => new Faker<Proposta>()
                                .RuleFor(p => p.Id, f2 => f2.Random.Int(1, 1000))
                                .RuleFor(p => p.NumeroProposta, f2 => f2.Lorem.Sentence())
                                .RuleFor(p => p.ValorCobertura, f2 => f2.Finance.Amount())
                                .RuleFor(p => p.Cliente, f2 => new Faker<Cliente>()
                                    .RuleFor(c => c.Id, f3 => f3.Random.Int(1, 1000))
                                    .RuleFor(c => c.Nome, f3 => f3.Name.FullName())
                                    .RuleFor(c => c.Email, f3 => f3.Internet.Email())
                                    .Generate())
                                .Generate());
                        }).Generate(3);

            var dto = Fixture.CreateMany<ApoliceDTO>(3).ToList();

            _repositorioMock.Setup(r => r.ObterDadosContratacaoClienteAsync())
                            .ReturnsAsync(entities);

            _mapperMock.Setup(m => m.Map<List<ApoliceDTO>>(entities))
                       .Returns(dto);

            var result = await _app.ObterDadosContratacaoClienteAsync();

            Assert.AreEqual(3, result.Count);
        }


        [Test]
        public async Task ObterPorIdAsync_DeveRetornarLista()
        {
            var dto = Fixture.Create<ApoliceDTO>();
            var entity = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create<Apolice>();

            _repositorioMock.Setup(r => r.ObterPorIdAssyn(entity.Id))
                            .ReturnsAsync(entity);

            _mapperMock.Setup(m => m.Map<ApoliceDTO>(entity))
                       .Returns(dto);

            var result = await _app.ObterPorIdAssyn(entity.Id);

            Assert.NotNull(result);
        }




    }
}
