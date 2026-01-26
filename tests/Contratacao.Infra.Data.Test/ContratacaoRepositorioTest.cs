using AutoFixture;
using AutoFixture.AutoMoq;
using Contratacao.Domain.Entidades;
using Contratacao.Infra.Data.Contexto;
using Contratacao.Infra.Data.Repositorio;
using NUnit.Framework;

namespace Contratacao.Infra.Data.Test
{
    public class ContratacaoRepositorioTest
    {
        private ApoliceRepositorio _repositorio;
        private ContratacaoDbContext _context;
        protected IFixture Fixture = null!;



        [SetUp]
        public void ResetDatabase()
        {
            Fixture = new Fixture()
               .Customize(new AutoMoqCustomization
               {
                   ConfigureMembers = true
               });
            _context = ContratacaoDbContextTest.CreateContext();
            _repositorio = new ApoliceRepositorio(_context);

        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task AdicionarAsync_DevePersistir()
        {
            var apolice = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();

            await _repositorio.AdicionarAsync(apolice);

            Assert.AreEqual(1, _context.Set<Apolice>().Count());
        }


        [Test]
        public async Task AtualizarAsync_DevePersistir()
        {

            var apolice = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();
            apolice.NumeroApolice = "Atualizado-123";
            await _repositorio.AdicionarAsync(apolice);
            await _repositorio.AtualizarAsync(apolice, apolice.Id);

            Assert.AreEqual(1, _context.Set<Apolice>().Count());


        }

        [Test]
        public async Task AtualizarAsync_QuandoEntidadeNaoEstaRastreada_DeveAtualizar()
        {
            // Arrange
            var apoliceOriginal = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();

            await _repositorio.AdicionarAsync(apoliceOriginal);

            _context.ChangeTracker.Clear();

            // Nova instância com o MESMO Id
            var apoliceAtualizada = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create(); ;
            apoliceAtualizada.Id = apoliceOriginal.Id;
            apoliceAtualizada.NumeroApolice = "ALTERADO";

            // Act
            var result = await _repositorio.AtualizarAsync(apoliceAtualizada, apoliceAtualizada.Id);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("ALTERADO", result.NumeroApolice);
        }




        [Test]
        public async Task ExcluirAsync_DevePersistir()
        {
            var apolice = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();
            await _repositorio.AdicionarAsync(apolice);

            await _repositorio.ExcluirAsync(apolice.Id);

            Assert.AreEqual(0, _context.Set<Apolice>().Count());
        }

        [Test]
        public async Task ObterPorIdAsync_DevePersistir()
        {
            var apolice = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();

            await _repositorio.AdicionarAsync(apolice);
            var retorno = await _repositorio.ObterPorIdAsync(apolice.Id);

            Assert.NotNull(retorno);


        }

       

        [Test]
        public async Task ObterTodosdAsync_DevePersistir()
        {
            var apolice = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();
            await _repositorio.AdicionarAsync(apolice);
            var retorno = await _repositorio.ObterTodosAsync();

            Assert.AreEqual(1, _context.Set<Apolice>().Count());
        }

        [Test]
        public async Task ObterDadosContratacaoClienteAsync_DevePersistir()
        {
            var apolice = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();
            await _repositorio.AdicionarAsync(apolice);
            var retorno = await _repositorio.ObterDadosContratacaoClienteAsync();

            Assert.AreEqual(1, _context.Set<Apolice>().Count());
        }

        [Test]
        public async Task ObterPorFiltroAsync_FiltraCorretamente()
        {


            var apolice1 = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();
            apolice1.NumeroApolice = "A123";

            var proposta2 = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();
            proposta2.NumeroApolice = "B456";

            await _context.Set<Apolice>().AddRangeAsync(apolice1, proposta2);
            await _context.SaveChangesAsync();

            // Act - filtra por NumeroProposta
            var result = await _repositorio.ObterPorFiltroAsync(p => p.NumeroApolice.StartsWith("A"));

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("A123", result.First().NumeroApolice);
        }


    }
}
