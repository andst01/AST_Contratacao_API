using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;

namespace Contratacao.Application.Test
{
    public abstract class AppBaseTest<TApp>
        where TApp : class
    {
        protected IFixture Fixture = null!;

        [SetUp]
        public virtual void BaseSetup()
        {
            Fixture = new Fixture()
                .Customize(new AutoMoqCustomization
                {
                    ConfigureMembers = true
                });
        }

        protected Mock<T> FreezeMock<T>() where T : class
        {
            return Fixture.Freeze<Mock<T>>();
        }

        protected TApp CreateSut()
        {
            return Fixture.Create<TApp>();
        }
    }

}
