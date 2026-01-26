using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Contratacao.Application.DTO;
using Contratacao.Domain.Entidades;
using Contratacao.Infra.CrossCuting.AutoMapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Infra.CrossCuting.Test
{
    [TestFixture]
    public class DomainToDTOMappingProfileTest
    {
        private readonly IMapper _mapper;

        protected readonly IFixture Fixture; 

        public DomainToDTOMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToDTOMappingProfile>();
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
        public void Proposta_To_PropostaViewModel_DeveMapearTodasPropriedades()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var entidade = Fixture.Build<Apolice>()
                                    .Without(p => p.Proposta)
                                    .Create();


            // Act
            var dto = _mapper.Map<ApoliceDTO>(entidade);

            // Assert
            Assert.NotNull(dto);

        }
    }
}
