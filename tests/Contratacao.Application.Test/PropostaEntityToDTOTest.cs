using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces.Map;
using Contratacao.Application.Map;
using Contratacao.Domain.Entidades;
using Moq;
using NUnit.Framework;

namespace Contratacao.Application.Test
{
    [TestFixture]
    public class PropostaEntityToDTOTest
    {
        private Mock<IMapBase<ClienteDTO, Cliente>> _clienteMapper;
        private PropostaEntityToDTO _mapper;


        [SetUp]
        public void Setup()
        {
            _clienteMapper = new Mock<IMapBase<ClienteDTO, Cliente>>();
            _mapper = new PropostaEntityToDTO(_clienteMapper.Object);
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
        public void Map_QuandoPropostaNaoTemCliente_DeveMapearCamposERetornarClienteNulo()
        {
            // Arrange
            var proposta = new Proposta
            {
                Id = 1,
                NumeroProposta = "PR-123",
                TipoSeguro = "Auto",
                Status = Domain.Enums.EnumStatusProposta.Aprovada,
                DataCriacao = DateTime.Now,
                DataValidade = DateTime.Now.AddMonths(6),
                Premio = 1200.00m,
                ValorCobertura = 30000.00m,
                FormaPagamento = "Boleto",
                QuantidadeParcelas = 6,
                CanalVenda = "Online",
                Observacoes = "Sem observações",
                IdCliente = 5,
                Cliente = null // Cenário sem cliente
            };
            // Act
            var resultado = _mapper.Map(proposta);
            // Assert
            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado.Id, Is.EqualTo(proposta.Id));
            Assert.That(resultado.NumeroProposta, Is.EqualTo(proposta.NumeroProposta));
            Assert.That(resultado.TipoSeguro, Is.EqualTo(proposta.TipoSeguro));
            Assert.That(resultado.Status, Is.EqualTo((int)proposta.Status));
            Assert.That(resultado.DataCriacao, Is.EqualTo(proposta.DataCriacao));
            Assert.That(resultado.DataValidade, Is.EqualTo(proposta.DataValidade));
            Assert.That(resultado.Premio, Is.EqualTo(proposta.Premio));
            Assert.That(resultado.ValorCobertura, Is.EqualTo(proposta.ValorCobertura));
            Assert.That(resultado.FormaPagamento, Is.EqualTo(proposta.FormaPagamento));
            Assert.That(resultado.QuantidadeParcelas, Is.EqualTo(proposta.QuantidadeParcelas));
            Assert.That(resultado.CanalVenda, Is.EqualTo(proposta.CanalVenda));
            Assert.That(resultado.Observacoes, Is.EqualTo(proposta.Observacoes));
            Assert.That(resultado.IdCliente, Is.EqualTo(proposta.IdCliente));
            Assert.That(resultado.Cliente, Is.Null);
        }
    }
}
