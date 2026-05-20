using AutoFixture;
using AutoFixture.AutoMoq;
using Castle.Components.DictionaryAdapter.Xml;
using Contratacao.Domain.Entidades;
using Contratacao.Infra.Data.Contexto;
using Contratacao.Infra.Data.Repositorio;
using Microsoft.EntityFrameworkCore;
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

            var entry = _context.Entry(apolice);

            Assert.AreEqual(EntityState.Added, entry.State);
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

            var entry = _context.Entry(apolice);

            Assert.AreEqual(EntityState.Modified, entry.State);


        }



        [Test]
        public async Task ExcluirAsync_DevePersistir()
        {
            var apolice = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();
            await _repositorio.AdicionarAsync(apolice);

            await _repositorio.ExcluirAsync(apolice.Id);

            var entry = _context.Entry(apolice);

            Assert.AreEqual(0, _context.Set<Apolice>().Count());
        }

        [Test]
        public async Task ObterPorIdAsync_DevePersistir()
        {
            var apolice = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();

            await _repositorio.AdicionarAsync(apolice);
            await _repositorio.SaveChangesAsync();

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
            await _repositorio.SaveChangesAsync();

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
            await _repositorio.SaveChangesAsync();

            var retorno = await _repositorio.ObterContratacaoPropostaClienteAsync();

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

        [Test]
        public async Task ObterPorFiltroAsync_FiltraPorDataCriacao()
        {
            var apolice1 = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();
            apolice1.DataContratacao = new DateTime(2024, 1, 1);
            var apolice2 = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();
            apolice2.DataContratacao = new DateTime(2024, 6, 1);
            await _context.Set<Apolice>().AddRangeAsync(apolice1, apolice2);
            await _context.SaveChangesAsync();
            // Act - filtra por DataContratacao
            var result = await _repositorio.ObterPorFiltroAsync(p => p.DataContratacao >= new DateTime(2024, 5, 1));
            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(new DateTime(2024, 6, 1), result.First().DataContratacao);


        }

        [Test]
        public async Task ObterContratacaoPropostaClientePorIdAsync_Test()
        {
            var apolice = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create();
            var proposta = Fixture.Build<Proposta>()
                                .Without(p => p.Cliente)
                                .Without(p => p.Apolice)
                                .Create();
            var cliente = Fixture.Build<Cliente>()
                                .Without(c => c.Propostas)
                                .Create();

            proposta.Cliente = cliente;
            apolice.Proposta = proposta;

            await _context.Set<Cliente>().AddAsync(cliente);
            await _context.Set<Proposta>().AddAsync(proposta);
            await _context.Set<Apolice>().AddAsync(apolice);

            await _context.SaveChangesAsync();

            await _context.Set<Apolice>()
                                  .Include(a => a.Proposta)
                                  .ThenInclude(p => p.Cliente)
                                  .Where(x => x.Id == apolice.Id)
                                  .FirstOrDefaultAsync();
            // Act
            var result = await _repositorio.ObterContratacaoPropostaClientePorIdAsync(apolice.Id);
            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(apolice.Id, result.Id);
        }

        [Test]
        [TestCase(null, null, 1)]
        [TestCase("2024-01-01", null, 1)]
        [TestCase(null, "A123", 1)]
        public async Task ObterTodosComFiltroAsync_Test(DateTime? dataFiltro, string? numeroApolice, int status)
        {
            var apolices = Fixture.Build<Apolice>()
                .With(x => x.NumeroApolice, numeroApolice ?? "Teste")
                .With(x => x.DataContratacao, dataFiltro ?? DateTime.Now)
                               .Without(p => p.Proposta)
                               .CreateMany(3).ToList();
            var propostas = Fixture.Build<Proposta>()
                                .Without(p => p.Cliente)
                                .Without(p => p.Apolice)
                                .CreateMany(3).ToList();
            var clientes = Fixture.Build<Cliente>()
                                .Without(c => c.Propostas)
                                .CreateMany(3).ToList();

            propostas = clientes.Select((cliente, index) =>
            {
                var proposta = propostas[index];
                proposta.Cliente = cliente;
                return proposta;
            }).ToList();

            apolices = propostas.Select((proposta, index) =>
            {
                var apolice = apolices[index];
                apolice.Proposta = proposta;
                return apolice;
            }).ToList();

            await _context.Set<Cliente>().AddRangeAsync(clientes);
            await _context.Set<Proposta>().AddRangeAsync(propostas);
            await _context.Set<Apolice>().AddRangeAsync(apolices);

            await _context.SaveChangesAsync();

            var result = await _repositorio.ObterTodosComFiltroAsync(dataFiltro, numeroApolice, status);
            Assert.NotNull(result);

            // Act

        }


        //[Test]
        //public async Task ObterPorFiltroAsync_FiltraPorStatus()
        //{
        //    var apolice1 = Fixture.Build<Apolice>()
        //                        .Without(p => p.Proposta)
        //                        .Create();
        //    apolice1.Status = Domain.Enums.EnumStatusApolice.Ativa;
        //    var apolice2 = Fixture.Build<Apolice>()
        //                        .Without(p => p.Proposta)
        //                        .Create();
        //    apolice2.Status = Domain.Enums.EnumStatusApolice.Cancelada;
        //    await _context.Set<Apolice>().AddRangeAsync(apolice1, apolice2);
        //    await _context.SaveChangesAsync();
        //    // Act - filtra por Status
        //    var result = await _repositorio.ObterPorFiltroAsync(p => p.Status == Domain.Enums.EnumStatusApolice.Ativa);
        //    // Assert
        //    Assert.AreEqual(1, result.Count());
        //    Assert.AreEqual(Domain.Enums.EnumStatusApolice.Ativa, result.First().Status);

        //}

    }


}
