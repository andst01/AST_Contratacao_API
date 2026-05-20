using Contratacao.Application.Map;
using Contratacao.Domain.Entidades;
using NUnit.Framework;

namespace Contratacao.Application.Test
{
    [TestFixture]
    public class ClienteEntityToDTOTest
    {
        private readonly ClienteEntityToDTO _mapper;

        public ClienteEntityToDTOTest()
        {
            _mapper = new ClienteEntityToDTO();
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
            var cliente = new Cliente
            {
                Id = 1,
                Nome = "John Doe",
                CpfCnpj = "123.456.789-00",
                DataNascimento = new DateTime(1980, 1, 1),
                Email = "john.doe@example.com",
                Telefone = "(11) 99999-9999",
                Endereco = "123 Main St",
                Cidade = "Anytown",
                Estado = "CA",
                Cep = "12345-678"
            };

            // Act
            var resultado = _mapper.Map(cliente);

            // Assert
            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado.Id, Is.EqualTo(cliente.Id));
            Assert.That(resultado.Nome, Is.EqualTo(cliente.Nome));
            Assert.That(resultado.CpfCnpj, Is.EqualTo(cliente.CpfCnpj));
            Assert.That(resultado.DataNascimento, Is.EqualTo(cliente.DataNascimento));
            Assert.That(resultado.Email, Is.EqualTo(cliente.Email));
            Assert.That(resultado.Telefone, Is.EqualTo(cliente.Telefone));
            Assert.That(resultado.Endereco, Is.EqualTo(cliente.Endereco));
            Assert.That(resultado.Cidade, Is.EqualTo(cliente.Cidade));
            Assert.That(resultado.Estado, Is.EqualTo(cliente.Estado));
            Assert.That(resultado.Cep, Is.EqualTo(cliente.Cep));
        }
    }
}
