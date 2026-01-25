using Contratacao.Api.Controllers;
using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Contratacao.Api.Test
{
    [TestFixture]
    public class PropostaControllerTest
    {
        private Mock<IApoliceApp> _mockApp;
        private Mock<ILogger<ContratacaoController>> _mockLogger;
        private ContratacaoController _controller;

        [SetUp]
        public void Setup()
        {
            _mockApp = new Mock<IApoliceApp>();
            _mockLogger = new Mock<ILogger<ContratacaoController>>();

            _controller = new ContratacaoController(_mockApp.Object, _mockLogger.Object);
        }

        [Test]
        public async Task ObterPorId_DeveRetornarOkComProposta()
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
        public async Task New_DeveAdicionarPropostaERetornarOk()
        {
            // Arrange
            var request = new ApoliceDTO { Id = 1, NumeroApolice = "PROP-001" };

            _mockApp.Setup(a => a.AdicionarAsync(request))
                    .ReturnsAsync(request);

            // Act
            var result = await _controller.New(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(request, okResult.Value);
        }

        [Test]
        public async Task Update_DeveAtualizarPropostaERetornarOk()
        {
            // Arrange
            var request = new ApoliceDTO { Id = 1, NumeroApolice = "PROP-001" };

            _mockApp.Setup(a => a.AtualizarAsync(request, request.Id))
                    .ReturnsAsync(request);

            // Act
            var result = await _controller.Update(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(request, okResult.Value);
        }
    }
}
