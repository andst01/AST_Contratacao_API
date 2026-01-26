using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces;
using Contratacao.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace Contratacao.Api.Controllers
{
    public class ContratacaoController : ControllerBase
    {
        private readonly IApoliceApp _application;
        private readonly IApoliceService _service;
        private readonly ILogger<ContratacaoController> _logger;

        public ContratacaoController(IApoliceApp application,
                                     IApoliceService service,
                                     ILogger<ContratacaoController> logger)
        {
            _application = application;
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("Obter/{id}")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            _logger.LogInformation("Obtendo contratação com ID: {Id} ", id);
            return Ok(await _application.ObterPorIdAssyn(id));
        }

        [HttpGet]
        [Route("ObterTodos")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterTodos()
        {
            _logger.LogInformation("Obtendo todas as contratações");
            return Ok(await _application.ObterTodosAsync());
        }

        [HttpGet]
        [Route("ObterDadosContratacaoCliente")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterDadosContratacaoCliente()
        {
            _logger.LogInformation("Obtendo todas as contratações cliente e proposta");
            return Ok(await _application.ObterDadosContratacaoClienteAsync());
        }

        [HttpPost]
        [Route("Novo")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Novo([FromBody] ApoliceDTO request)
        {
            _logger.LogInformation("Adicionando nova contratação");
            return Ok(await _service.CriarApoliceAsync(request));
        }

        [HttpPut]
        [Route("Atualizar")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Atualizar([FromBody] ApoliceDTO request)
        {
            _logger.LogInformation("Atualizando contratação com ID: {Id} ", request.Id);
            return Ok(await _application.AtualizarAsync(request, request.Id));
        }

        [HttpDelete]
        [Route("Excluir/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Excluir(int id)
        {
            _logger.LogInformation("Excluindo contratação com ID: {Id} ", id);
            return Ok(await _application.ExcluirAsync(id));
        }
    }
}
