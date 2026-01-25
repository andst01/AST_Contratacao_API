using AutoFixture;
using AutoMapper;
using Contratacao.Application.DTO;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace Contratacao.Application.Test
{
    public class ApoliceAppTests : AppBaseTest<ApoliceApp>
    {
        private Mock<IRepositorioBase<Apolice>> _repositorioMock = null!;
        private Mock<IMapper> _mapperMock = null!;
        private ApoliceApp _app = null!;


        [SetUp]
        public void Setup()
        {
            _repositorioMock = FreezeMock<IRepositorioBase<Apolice>>();
            _mapperMock = FreezeMock<IMapper>();

            _app = CreateSut();
        }

        [Test]
        public async Task AdicionarAsync_DeveAdicionarERetornarViewModel()
        {
            var dto = Fixture.Create<ApoliceDTO>();
            var entity = Fixture.Create<Apolice>();

            _mapperMock
                .Setup(m => m.Map<Apolice>(dto))
                .Returns(entity);

            _repositorioMock
                .Setup(r => r.AdicionarAsync(entity))
                .ReturnsAsync(entity);

            _mapperMock
                .Setup(m => m.Map<ApoliceDTO>(entity))
                .Returns(dto);

            var result = await _app.AdicionarAsync(dto);

            Assert.NotNull(result);
            _repositorioMock.Verify(r => r.AdicionarAsync(entity), Times.Once);
        }

        [Test]
        public async Task AtualizarAsync_ComId_DeveAtualizar()
        {
            var dto = Fixture.Create<ApoliceDTO>();
            var entity = Fixture.Create<Apolice>();
            var id = Fixture.Create<int>();

            _mapperMock.Setup(m => m.Map<Apolice>(dto))
                       .Returns(entity);

            _repositorioMock.Setup(r => r.AtualizarAsync(entity, id))
                            .ReturnsAsync(entity);

            _mapperMock.Setup(m => m.Map<ApoliceDTO>(entity))
                       .Returns(dto);

            var result = await _app.AtualizarAsync(dto, id);

            Assert.NotNull(result);
            _repositorioMock.Verify(r => r.AtualizarAsync(entity, id), Times.Once);
        }

        [Test]
        public async Task ExcluirAsync_DeveChamarRepositorio()
        {
            var id = Fixture.Create<int>();

            _repositorioMock
                .Setup(r => r.ExcluirAsync(id))
                .ReturnsAsync(1);

            var result = await _app.ExcluirAsync(id);

            Assert.AreEqual(1, result);
            _repositorioMock.Verify(r => r.ExcluirAsync(id), Times.Once);
        }

        [Test]
        public async Task ObterTodosAsync_DeveRetornarLista()
        {
            var entities = Fixture.CreateMany<Apolice>(3).ToList();
            var dto = Fixture.CreateMany<ApoliceDTO>(3).ToList();

            _repositorioMock.Setup(r => r.ObterTodosAsync())
                            .ReturnsAsync(entities);

            _mapperMock.Setup(m => m.Map<List<ApoliceDTO>>(entities))
                       .Returns(dto);

            var result = await _app.ObterTodosAsync();

            Assert.AreEqual(3, result.Count);
        }


        [Test]
        public async Task ObterPorIdAsync_DeveRetornarLista()
        {
            var dto = Fixture.Create<ApoliceDTO>();
            var entity = Fixture.Create<Apolice>();

            _repositorioMock.Setup(r => r.ObterPorIdAssyn(entity.Id))
                            .ReturnsAsync(entity);

            _mapperMock.Setup(m => m.Map<ApoliceDTO>(entity))
                       .Returns(dto);

            var result = await _app.ObterPorIdAssyn(entity.Id);

            Assert.NotNull(result);
        }




    }
}
