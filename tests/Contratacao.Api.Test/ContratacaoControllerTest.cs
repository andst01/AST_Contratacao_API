using AutoFixture;
using AutoFixture.AutoMoq;
using Contratacao.Api.Controllers;
using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces;
using Contratacao.Application.Interfaces.Service;
using Contratacao.Application.Request;
using Contratacao.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Contratacao.Api.Test
{
    [TestFixture]
    public class PropostaControllerTest
    {
        private IFixture Fixture;
        private Mock<IApoliceApp> _mockApp;
        private Mock<IApoliceService> _mockService;
        private Mock<ILogger<ContratacaoController>> _mockLogger;
        private ContratacaoController _controller;

        [SetUp]
        public void Setup()
        {
            Fixture = new Fixture()
                .Customize(new AutoMoqCustomization
                {
                    ConfigureMembers = true
                });

            _mockApp = new Mock<IApoliceApp>();
            _mockLogger = new Mock<ILogger<ContratacaoController>>();
            _mockService = new Mock<IApoliceService>();
            _controller = new ContratacaoController(_mockApp.Object, _mockService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task ObterPorId_DeveRetornarOk()
        {
            // Arrange
            var id = 1;
            var propostaVm = new ApoliceDTO { Id = id, NumeroApolice = "PROP-001" };

            _mockApp.Setup(a => a.ObterPorIdAssyn(id))
                    .ReturnsAsync(propostaVm);

            // Act
            var result = await _controller.ObterPorId(id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(propostaVm, okResult.Value);
        }

        [Test]
        public async Task ObterContratacaoPropostaClientePorId_DeveRetornarOk()
        {
            // Arrange
            var id = 1;
            var propostaVm = new ApoliceDTO { Id = id, NumeroApolice = "PROP-001" };
            _mockApp.Setup(a => a.ObterContratacaoPropostaClientePorIdAsync(id))
                    .ReturnsAsync(propostaVm);
            // Act
            var result = await _controller.ObterContratacaoPropostaClientePorId(id);
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(propostaVm, okResult.Value);
        }

        [Test]
        public async Task ObterTodos_DeveRetornarOkComLista()
        {
            // Arrange
            var lista = new List<ApoliceDTO>
        {
            new ApoliceDTO { Id = 1, NumeroApolice = "PROP-001" },
            new ApoliceDTO { Id = 2, NumeroApolice = "PROP-002" }
        };

            _mockApp.Setup(a => a.ObterTodosAsync())
                    .ReturnsAsync(lista);

            // Act
            var result = await _controller.ObterTodos();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(lista, okResult.Value);
        }

        [Test]
        public async Task ObterDadosContratacaoCliente_DeveRetornarOkComLista()
        {
            // Arrange
        
            var lista = Fixture.Build<ApoliceDTO>()
                            .Without(x => x.Proposta)
                            .CreateMany(3)
                            .ToList();

            _mockApp.Setup(a => a.ObterContratacaoPropostaClienteAsync())
                    .ReturnsAsync(lista);

            // Act
            var result = await _controller.ObterDadosContratacaoCliente();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(lista, okResult.Value);
        }


        [Test]
        [TestCase(null, null, 1)]
        [TestCase("2024-01-01", null, 1)]
        [TestCase(null, "A123", 1)]
        public async Task ObterTodosComFiltroAsync_Test(DateTime? dataFiltro, string? numeroApolice, int status)
        {
            // Arrange

            //var lista = Fixture.Build<ApoliceDTO>()
                            
            //                .Without(x => x.Proposta)
            //                .CreateMany(3)
            //                .ToList();

            var lista = Fixture.Build<ApoliceDTO>()
                .With(x => x.CodigoStatus, status)
               .With(x => x.NumeroApolice, numeroApolice ?? "Teste")
               .With(x => x.DataContratacao, dataFiltro ?? DateTime.Now)
                              .Without(p => p.Proposta)
                              .CreateMany(3).ToList();

            _mockApp.Setup(a => a.ObterTodosComFiltroAsync(dataFiltro, numeroApolice, status))
                    .ReturnsAsync(lista);

            // Act
            var result = await _controller.ObterTodosComFiltroAsync(dataFiltro, numeroApolice, status);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            //Assert.AreEqual(lista, okResult.Value);
        }

        [Test]
        public async Task New_DeveAdicionarPropostaERetornarOk()
        {
            // Arrange
            var request = Fixture.Create<ApoliceRequest>();
            var response = new ApoliceDTO { Id = 1, NumeroApolice = "PROP-001" };

            _mockService.Setup(x => x.CriarApoliceAsync(request))
                        .ReturnsAsync(response);
           

            // Act
            var result = await _controller.Novo(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(response, okResult.Value);
        }

        [Test]
        public async Task Update_DeveAtualizarERetornarOk()
        {
            // Arrange
            var request = Fixture.Create<ApoliceRequest>();
            var response = new ApoliceDTO { Id = 1, NumeroApolice = "PROP-001" };

            _mockApp.Setup(a => a.AtualizarAsync(request, request.Id))
                    .ReturnsAsync(response);

            // Act
            var result = await _controller.Atualizar(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(response, okResult.Value);
        }

        [Test]
        public async Task Excluir_DeveExcluirERetornarOk()
        {
            // Arrange
            var response = new ApoliceDTO { Id = 1, NumeroApolice = "PROP-001" };

            _mockApp.Setup(a => a.ExcluirAsync(10))
                    .ReturnsAsync(response);

            // Act
            var result = await _controller.Excluir(response.Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            //Assert.AreEqual(request, okResult.Value);
        }
    }
}
