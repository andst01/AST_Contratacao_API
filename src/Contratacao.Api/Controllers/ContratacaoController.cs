using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contratacao.Api.Controllers
{
    public class ContratacaoController : Controller
    {
        private readonly IApoliceApp _application;
        private readonly ILogger<ContratacaoController> _logger;

        public ContratacaoController(IApoliceApp application,
                                     ILogger<ContratacaoController> logger)
        {
            _application = application;
            _logger = logger;
        }

        [HttpGet]
        [Route("Obter/{id}")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            _logger.LogInformation("Obtendo proposta com ID: {Id} ", id);
            return Ok(await _application.ObterPorIdAssyn(id));
        }

        [HttpGet]
        [Route("ObterTodos")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterTodos()
        {
            _logger.LogInformation("Obtendo todas as propostas");
            return Ok(await _application.ObterTodosAsync());
        }

        [HttpGet]
        [Route("ObterDadosContratacaoCliente")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterDadosContratacaoCliente()
        {
            _logger.LogInformation("Obtendo todas as contratacoes cliente e proposta");
            return Ok(await _application.ObterDadosContratacaoClienteAsync());
        }

        [HttpPost]
        [Route("Novo")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> New([FromBody] ApoliceDTO request)
        {
            _logger.LogInformation("Adicionando nova proposta");
            return Ok(await _application.AdicionarAsync(request));
        }

        [HttpPut]
        [Route("Atualizar")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] ApoliceDTO request)
        {
            _logger.LogInformation("Atualizando proposta com ID: {Id} ", request.Id);
            return Ok(await _application.AtualizarAsync(request, request.Id));
        }
    }
}
