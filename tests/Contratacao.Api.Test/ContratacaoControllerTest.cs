using AutoFixture;
using AutoFixture.AutoMoq;
using Contratacao.Api.Controllers;
using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces;
using Contratacao.Application.Interfaces.Service;
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
        
            var lista = Fixture.Create<List<ApoliceDTO>>();

            _mockApp.Setup(a => a.ObterDadosContratacaoClienteAsync())
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
        public async Task New_DeveAdicionarPropostaERetornarOk()
        {
            // Arrange
            var request = new ApoliceDTO { Id = 1, NumeroApolice = "PROP-001" };

            _mockService.Setup(x => x.CriarApoliceAsync(request))
                        .ReturnsAsync(request);
           

            // Act
            var result = await _controller.Novo(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(request, okResult.Value);
        }

        [Test]
        public async Task Update_DeveAtualizarERetornarOk()
        {
            // Arrange
            var request = new ApoliceDTO { Id = 1, NumeroApolice = "PROP-001" };

            _mockApp.Setup(a => a.AtualizarAsync(request, request.Id))
                    .ReturnsAsync(request);

            // Act
            var result = await _controller.Atualizar(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(request, okResult.Value);
        }

        [Test]
        public async Task Excluir_DeveExcluirERetornarOk()
        {
            // Arrange
            var request = new ApoliceDTO { Id = 1, NumeroApolice = "PROP-001" };

            _mockApp.Setup(a => a.ExcluirAsync(request.Id))
                    .ReturnsAsync(0);

            // Act
            var result = await _controller.Excluir(request.Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            //Assert.AreEqual(request, okResult.Value);
        }
    }
}
