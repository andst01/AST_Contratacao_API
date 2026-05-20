using AutoFixture;
using Bogus;
using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces.Map;
using Contratacao.Application.Request;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Enums;
using Contratacao.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace Contratacao.Application.Test
{
    public class ApoliceAppTests : AppBaseTest<ApoliceApp>
    {
        private Mock<IApoliceRepoitorio> _repositorioMock = null!;
        private ApoliceApp _app = null!;
        private Mock<IMapBase<Apolice, ApoliceRequest>> _mapRequestToEntityMock = null!;
        private Mock<IMapBase<PropostaDTO, Proposta>> _mapPropostaToDtoMock = null!;
        private Mock<IMapBase<ClienteDTO, Cliente>> _mapClienteToDtoMock = null!;
        private Mock<IMapBase<ApoliceDTO, Apolice>> _mapEntityToDtoMock = null!;
        private Mock<IMapBase<List<ApoliceDTO>, List<Apolice>>> _mapListEntityToDtoMock = null!;

        [SetUp]
        public void Setup()
        {
            _repositorioMock = FreezeMock<IApoliceRepoitorio>();
            _mapPropostaToDtoMock = new Mock<IMapBase<PropostaDTO, Proposta>>();
            _mapRequestToEntityMock = new Mock<IMapBase<Apolice, ApoliceRequest>>();
            _mapEntityToDtoMock = new Mock<IMapBase<ApoliceDTO, Apolice>>();
            _mapClienteToDtoMock = new Mock<IMapBase<ClienteDTO, Cliente>>();
            _mapListEntityToDtoMock = new Mock<IMapBase<List<ApoliceDTO>, List<Apolice>>>();
            _app = new ApoliceApp(_repositorioMock.Object, 
                                  _mapRequestToEntityMock.Object, 
                                  _mapEntityToDtoMock.Object);
        }

        [Test]
        public async Task AdicionarAsync_DeveAdicionarERetornarViewModel()
        {
            var request = Fixture.Create<ApoliceRequest>();
            var dto = Fixture.Build<ApoliceDTO>()
                .Without(d => d.Proposta)
                .Create();

            var entity = Fixture.Build<Apolice>()
                        .Without(p => p.Proposta).Create();

            //_mapperMock
            //    .Setup(m => m.Map<Apolice>(request))
            //    .Returns(entity);

            _mapRequestToEntityMock
                .Setup(m => m.Map(request))
                .Returns(entity);

            _repositorioMock
                .Setup(r => r.AdicionarAsync(entity))
                .ReturnsAsync(entity);

            //_mapperMock
            //    .Setup(m => m.Map<ApoliceDTO>(entity))
            //    .Returns(dto);

            _mapEntityToDtoMock
                .Setup(m => m.Map(entity))
                .Returns(dto);

            _repositorioMock.Setup(r => r.SaveChangesAsync())
               .ReturnsAsync(1);

            var result = await _app.AdicionarAsync(request);

            Assert.NotNull(result);
            _repositorioMock.Verify(r => r.AdicionarAsync(entity), Times.Once);
        }

        [Test]
        public async Task AtualizarAsync_ComId_DeveAtualizar()
        {
            var request = Fixture.Create<ApoliceRequest>();
            var dto = Fixture.Build<ApoliceDTO>()
                .Without(d => d.Proposta)
                .Create();

            var entity = Fixture.Build<Apolice>()
                        .Without(p => p.Proposta).Create();
            var id = Fixture.Create<int>();

            //_mapperMock.Setup(m => m.Map<Apolice>(request))
            //           .Returns(entity);

            _mapRequestToEntityMock.Setup(m => m.Map(request))
                       .Returns(entity);

            _repositorioMock.Setup(r => r.AtualizarAsync(entity, id))
                            .ReturnsAsync(entity);

            //_mapperMock.Setup(m => m.Map<ApoliceDTO>(entity))
            //           .Returns(dto);

            _mapEntityToDtoMock.Setup(m => m.Map(entity))
                       .Returns(dto);

            _repositorioMock.Setup(r => r.SaveChangesAsync())
               .ReturnsAsync(1);

            var result = await _app.AtualizarAsync(request, id);

            Assert.NotNull(result);
            _repositorioMock.Verify(r => r.AtualizarAsync(entity, id), Times.Once);
        }

        [Test]
        public async Task ExcluirAsync_DeveChamarRepositorio()
        {
            var id = Fixture.Create<int>();

            _repositorioMock
                .Setup(r => r.ExcluirAsync(id))
                .Returns(Task.CompletedTask);

            _repositorioMock.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var result = await _app.ExcluirAsync(id);

            Assert.AreEqual(true, result.Mensagem.Sucesso);
            _repositorioMock.Verify(r => r.ExcluirAsync(id), Times.Once);
        }

        [Test]
        public async Task ObterContratacaoPropostaClienteAsync_DeveRetornarLista()
        {
            var entities = new Faker<Apolice>()
                        .RuleFor(a => a.Id, f => f.Random.Int(1, 1000))
                        .RuleFor(a => a.NumeroApolice, f => f.Finance.Account())
                        .RuleFor(a => a.Status, f => f.PickRandom<EnumStatusApolice>())
                        .RuleFor(a => a.DataInicioVigencia, f => f.Date.Past())
                        .RuleFor(a => a.DataFimVigencia, f => f.Date.Future())
                        .RuleFor(a => a.PremioFinal, f => f.Finance.Amount())
                        .RuleFor(a => a.IdProposta, f => f.Random.Int(1, 1000))
                        .RuleSet("WithProposta", rules =>
                        {
                            rules.RuleFor(a => a.Proposta, f => new Faker<Proposta>()
                                .RuleFor(p => p.Id, f2 => f2.Random.Int(1, 1000))
                                .RuleFor(p => p.NumeroProposta, f2 => f2.Lorem.Sentence())
                                .RuleFor(p => p.ValorCobertura, f2 => f2.Finance.Amount())
                                .RuleFor(p => p.Cliente, f2 => new Faker<Cliente>()
                                    .RuleFor(c => c.Id, f3 => f3.Random.Int(1, 1000))
                                    .RuleFor(c => c.Nome, f3 => f3.Name.FullName())
                                    .RuleFor(c => c.Email, f3 => f3.Internet.Email())
                                    .Generate())
                                .Generate());
                        }).Generate(3);

            var dto = Fixture.Build<ApoliceDTO>()
                .Without(d => d.Proposta)
                .CreateMany(3)
                .ToList();

            var dtoProposta = Fixture.Build<PropostaDTO>()
                                .Without(p => p.Apolice)
                                .Without(p => p.Cliente)
                                .Create();

            var dtoCliente = Fixture.Build<ClienteDTO>()
                                .Without(c => c.Propostas)
                                .Create();

            _mapPropostaToDtoMock.Setup(x => x.Map(It.IsAny<Proposta>()))
                .Returns(dtoProposta);

            _mapClienteToDtoMock.Setup(x => x.Map(It.IsAny<Cliente>()))
                .Returns(dtoCliente);

            _mapClienteToDtoMock.Setup(x => x.Map(It.IsAny<Cliente>()))
                .Returns(dtoCliente);

            _repositorioMock.Setup(r => r.ObterContratacaoPropostaClienteAsync())
                            .ReturnsAsync(entities);
            //_mapperMock.Setup(m => m.Map<List<ApoliceDTO>>(entities))
            //           .Returns(dto);
            var result = await _app.ObterContratacaoPropostaClienteAsync();
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public async Task ObterTodosComFiltroAsync_DeveRetornarLista()
        {
           // var filtro = Fixture.Create<ApoliceFiltroDTO>();
            var entities = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .CreateMany(3).ToList();
            var dto = new ApoliceDTO();
            var dtoLista = new List<ApoliceDTO>();
            var dtoProposta = Fixture.Build<PropostaDTO>()
                                .Without(p => p.Apolice)
                                .Without(p => p.Cliente)
                                .Create();


            dtoLista = entities.Select(x => Fixture.Build<ApoliceDTO>()
                                        .With(d => d.Proposta, dtoProposta)
                                        .With(d => d.Id, x.Id)
                                        .Create()).ToList();



            //var dto = Fixture.Build<ApoliceDTO>()
            //    .Without(d => d.Proposta)
            //    .CreateMany(3)
            //    .ToList();
            _repositorioMock.Setup(r => r.ObterTodosComFiltroAsync(It.IsAny<DateTime>(), 
                                                                   It.IsAny<string>(), 
                                                                   It.IsAny<int>()))
                                                .ReturnsAsync(entities);
            //_mapperMock.Setup(m => m.Map<List<ApoliceDTO>>(entities))
            //           .Returns(dto);

            entities.Select(x => _mapEntityToDtoMock.Setup(m => m.Map(x))
                       .Returns(dtoLista.FirstOrDefault(d => d.Id == x.Id)));

            //_mapListEntityToDtoMock.Setup(m => m.Map(entities))
            //           .Returns(dto);

            var result = await _app.ObterTodosComFiltroAsync(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<int>());
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public async Task ObterTodosAsync_DeveRetornarLista()
        {
            var entityProposta = Fixture.Build<Proposta>()
                                .Without(p => p.Apolice)
                                .Without(p => p.Cliente)
                                .Create();

            var dtoProposta = Fixture.Build<PropostaDTO>()
                                .Without(p => p.Apolice)
                                .Without(p => p.Cliente)
                                .Create();

            var entities = Fixture.Build<Apolice>()
                                .With(p => p.Proposta, entityProposta)
                                .CreateMany<Apolice>(3).ToList();

            var dto = Fixture.Build<ApoliceDTO>()
                .With(d => d.Proposta, dtoProposta)
                .CreateMany(3)
                .ToList();

            _repositorioMock.Setup(r => r.ObterTodosAsync())
                            .ReturnsAsync(entities);

            //_mapEntityToDtoMock.Setup(m => m.Map<List<ApoliceDTO>>(entities))
            //           .Returns(dto);

            var result = await _app.ObterTodosAsync();

            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public async Task ObterDadosContratacaoClienteAsync_DeveRetornarLista()
        {
          
            var entities = new Faker<Apolice>()
                        .RuleFor(a => a.Id, f => f.Random.Int(1, 1000))
                        .RuleFor(a => a.NumeroApolice, f => f.Finance.Account())
                        .RuleFor(a => a.Status, f => f.PickRandom<EnumStatusApolice>())
                        .RuleFor(a => a.DataInicioVigencia, f => f.Date.Past())
                        .RuleFor(a => a.DataFimVigencia, f => f.Date.Future())
                        .RuleFor(a => a.PremioFinal, f => f.Finance.Amount())
                        .RuleFor(a => a.IdProposta, f => f.Random.Int(1, 1000))
                        .RuleSet("WithProposta", rules =>
                        {
                            rules.RuleFor(a => a.Proposta, f => new Faker<Proposta>()
                                .RuleFor(p => p.Id, f2 => f2.Random.Int(1, 1000))
                                .RuleFor(p => p.NumeroProposta, f2 => f2.Lorem.Sentence())
                                .RuleFor(p => p.ValorCobertura, f2 => f2.Finance.Amount())
                                .RuleFor(p => p.Cliente, f2 => new Faker<Cliente>()
                                    .RuleFor(c => c.Id, f3 => f3.Random.Int(1, 1000))
                                    .RuleFor(c => c.Nome, f3 => f3.Name.FullName())
                                    .RuleFor(c => c.Email, f3 => f3.Internet.Email())
                                    .Generate())
                                .Generate());
                        }).Generate(3);

            var dto = Fixture.Build<ApoliceDTO>()
                .Without(d => d.Proposta)
                .CreateMany(3)
                .ToList();

            _repositorioMock.Setup(r => r.ObterContratacaoPropostaClienteAsync())
                            .ReturnsAsync(entities);

            //_mapperMock.Setup(m => m.Map<List<ApoliceDTO>>(entities))
            //           .Returns(dto);



            var result = await _app.ObterContratacaoPropostaClienteAsync();

            Assert.AreEqual(3, result.Count);
        }


        [Test]
        public async Task ObterPorIdAsync_DeveRetornarLista()
        {
            var dto = Fixture.Build<ApoliceDTO>()
                .Without(d => d.Proposta)
                .Create();

            var entity = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create<Apolice>();

            _repositorioMock.Setup(r => r.ObterPorIdAsync(entity.Id))
                            .ReturnsAsync(entity);

            //_mapperMock.Setup(m => m.Map<ApoliceDTO>(entity))
            //           .Returns(dto);

            _mapEntityToDtoMock.Setup(m => m.Map(entity))
                       .Returns(dto);

            var result = await _app.ObterPorIdAssyn(entity.Id);


            Assert.NotNull(result);
        }

        [Test]
        public async Task ObterContratacaoPropostaClientePorIdAsync_Test()
        {
            var dto = Fixture.Build<ApoliceDTO>()
                .Without(d => d.Proposta)
                .Create();
            var entity = Fixture.Build<Apolice>()
                                .Without(p => p.Proposta)
                                .Create<Apolice>();
            _repositorioMock.Setup(r => r.ObterContratacaoPropostaClientePorIdAsync(entity.Id))
                            .ReturnsAsync(entity);
            //_mapperMock.Setup(m => m.Map<ApoliceDTO>(entity))
            //           .Returns(dto);
            _mapEntityToDtoMock.Setup(m => m.Map(entity))
                       .Returns(dto);
            var result = await _app.ObterContratacaoPropostaClientePorIdAsync(entity.Id);
        }




    }
}
