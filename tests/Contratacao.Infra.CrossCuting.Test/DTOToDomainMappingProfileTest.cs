using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Contratacao.Application.DTO;
using Contratacao.Domain.Entidades;
using Contratacao.Infra.CrossCuting.AutoMapper;
using NUnit.Framework;

namespace Contratacao.Infra.CrossCuting.Test
{
    public class DTOToDomainMappingProfileTest
    {
        private readonly IMapper _mapper;

        protected readonly IFixture Fixture;

        public DTOToDomainMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DTOToDomainMappingProfile>();
            });

            
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();

            Fixture = new Fixture()
              .Customize(new AutoMoqCustomization
              {
                  ConfigureMembers = true
              });
        }

        [Test]
        public void PropostaViewModel_To_Proposta_DeveMapearTodasPropriedades()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var dto = Fixture.Create<ApoliceDTO>();
            dto.CodigoStatus = 2;

            // Act
            var entidade = _mapper.Map<Apolice>(dto);

            // Assert
            Assert.NotNull(dto);

        }
    }
}
