using Contratacao.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Text.Json;

namespace Contratacao.Api.Test
{
    [TestFixture]
    public class GenericExceptionFilterAttributeTest
    {
        private Mock<ILoggerFactory> _loggerFactoryMock;
        private Mock<ILogger<GenericExceptionFilterAttribute>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<GenericExceptionFilterAttribute>>();

            _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactoryMock
                .Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(_loggerMock.Object);
        }

        [Test]
        public void OnException_DeveRetornarStatus500EJsonDeErro()
        {
            // Arrange
            var exception = new Exception("Erro de teste");

            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(
                httpContext,
                new RouteData(),
                new ActionDescriptor()
            );

            var exceptionContext = new ExceptionContext(
                actionContext,
                new List<IFilterMetadata>()
            )
            {
                Exception = exception
            };

            var filter = new GenericExceptionFilterAttribute(_loggerFactoryMock.Object);

            // Act
            filter.OnException(exceptionContext);

            // Assert - Status Code
            Assert.AreEqual(StatusCodes.Status500InternalServerError,
                            exceptionContext.HttpContext.Response.StatusCode);

            // Assert - Result
            Assert.IsInstanceOf<ContentResult>(exceptionContext.Result);

            var contentResult = exceptionContext.Result as ContentResult;
            Assert.NotNull(contentResult);
            Assert.AreEqual("application/json", contentResult.ContentType);

            // Assert - Conteúdo JSON
            var json = JsonDocument.Parse(contentResult.Content);

            Assert.AreEqual(
                "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.",
                json.RootElement.GetProperty("Message").GetString()
            );

            Assert.AreEqual(
                "Erro de teste",
                json.RootElement.GetProperty("Detalhes").GetString()
            );

            // Assert - Logger chamado
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Ocorreu uma exceção não tratada")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once
            );
        }
    }
}
