using Contratacao.Infra.CrossCuting.Config;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Contratacao.Infra.CrossCuting.Test
{
    [TestFixture]
    public class SwaggerConfigTest
    {
        private IServiceCollection _services;

        [SetUp]
        public void Setup()
        {
            _services = new ServiceCollection();
        }

        [Test]
        public void AddSwaggerConfig_ServicesNull_DeveLancarArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                SwaggerConfig.AddSwaggerConfig(null));

            Assert.AreEqual("services", ex.ParamName);
        }

        [Test]
        public void AddSwaggerConfig_DeveRegistrarServicos()
        {
            // Act
            Assert.DoesNotThrow(() => SwaggerConfig.AddSwaggerConfig(_services));

            // Assert
            Assert.IsTrue(_services.Count > 0, "O container deve ter pelo menos um serviço registrado pelo AddSwaggerGen");
        }
    }
}
