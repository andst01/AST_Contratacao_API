using Contratacao.Application.Map;
using Contratacao.Application.Request;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Application.Test
{
    [TestFixture]
    public class ApoliceRequestToEntityTest
    {
        private ApoliceRequestToEntity _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new ApoliceRequestToEntity();
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
        public void Map_QuandoObjetoOrigemForValido_DeveMapearCampos()
        {
            // Arrange
            var request = new ApoliceRequest
            {
                IdProposta = 10,
                DataInicioVigencia = DateTime.Now,
                DataFimVigencia = DateTime.Now.AddYears(1),
                PremioFinal = 1500.00m,
                ValorCobertura = 50000.00m,
                FormaPagamento = "Cartao",
                QuantidadeParcelas = 12
            };
            // Act
            var resultado = _mapper.Map(request);
            // Assert
            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado.IdProposta, Is.EqualTo(request.IdProposta));
            Assert.That(resultado.DataInicioVigencia, Is.EqualTo(request.DataInicioVigencia));
            Assert.That(resultado.DataFimVigencia, Is.EqualTo(request.DataFimVigencia));
            Assert.That(resultado.PremioFinal, Is.EqualTo(request.PremioFinal));
            Assert.That(resultado.ValorCobertura, Is.EqualTo(request.ValorCobertura));
            Assert.That(resultado.FormaPagamento, Is.EqualTo(request.FormaPagamento));
            Assert.That(resultado.QuantidadeParcelas, Is.EqualTo(request.QuantidadeParcelas));

        }
    }
}
