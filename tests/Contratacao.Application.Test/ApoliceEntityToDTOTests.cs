using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces.Map;
using Contratacao.Application.Map;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Application.Test
{
    [TestFixture]
    public class ApoliceEntityToDTOTests
    {
        private Mock<IMapBase<PropostaDTO, Proposta>> _mockMapProposta;
        private ApoliceEntityToDTO _mapper;

        [SetUp]
        public void Setup()
        {
            // 1. Criamos o mock da dependência
            _mockMapProposta = new Mock<IMapBase<PropostaDTO, Proposta>>();

            // 2. Injetamos o mock no construtor da classe que vamos testar
            _mapper = new ApoliceEntityToDTO(_mockMapProposta.Object);
        }

        [Test]
        public void Map_QuandoObjetoOrigemForNulo_DeveRetornarNulo()
        {
            // Act
            var resultado = _mapper.Map(null);

            // Assert
            Assert.That(resultado, Is.Null);
        }

        [Test]
        public void Map_QuandoApoliceNaoTemProposta_DeveMapearCamposERetornarPropostaNula()
        {
            // Arrange
            var apolice = new Apolice
            {
                Id = 1,
                NumeroApolice = "AP-123",
                IdProposta = 10,
                DataInicioVigencia = DateTime.Now,
                DataFimVigencia = DateTime.Now.AddYears(1),
                PremioFinal = 1500.00m,
                ValorCobertura = 50000.00m,
                FormaPagamento = "Cartao",
                QuantidadeParcelas = 12,
                DataContratacao = DateTime.Now,
                Status = EnumStatusApolice.Ativa, // Supondo que seja um Enum
                Proposta = null // Cenário sem proposta
            };

            // Act
            var resultado = _mapper.Map(apolice);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(resultado, Is.Not.Null);
                Assert.That(resultado.Id, Is.EqualTo(apolice.Id));
                Assert.That(resultado.NumeroApolice, Is.EqualTo(apolice.NumeroApolice));
                Assert.That(resultado.IdProposta, Is.EqualTo(apolice.IdProposta));
                Assert.That(resultado.DataInicioVigencia, Is.EqualTo(apolice.DataInicioVigencia));
                Assert.That(resultado.DataFimVigencia, Is.EqualTo(apolice.DataFimVigencia));
                Assert.That(resultado.PremioFinal, Is.EqualTo(apolice.PremioFinal));
                Assert.That(resultado.ValorCobertura, Is.EqualTo(apolice.ValorCobertura));
                Assert.That(resultado.FormaPagamento, Is.EqualTo(apolice.FormaPagamento));
                Assert.That(resultado.QuantidadeParcelas, Is.EqualTo(apolice.QuantidadeParcelas));
                Assert.That(resultado.DataContratacao, Is.EqualTo(apolice.DataContratacao));
                Assert.That(resultado.CodigoStatus, Is.EqualTo((int)apolice.Status));
                Assert.That(resultado.Proposta, Is.Null);
            });

            // Garante que o mapper interno NÃO foi chamado já que a proposta era nula
            _mockMapProposta.Verify(m => m.Map(It.IsAny<Proposta>()), Times.Never);
        }


       

    }
}
