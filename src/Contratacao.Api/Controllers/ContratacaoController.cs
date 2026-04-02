using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces;
using Contratacao.Application.Interfaces.Service;
using Contratacao.Application.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Contratacao.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
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
        [Route("ObterContratacaoPropostaClientePorId/{id}")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterContratacaoPropostaClientePorId(int id)
        {
            _logger.LogInformation("Obtendo contratação com ID: {Id} ", id);
            return Ok(await _application.ObterContratacaoPropostaClientePorIdAsync(id));
        }

        [HttpGet]
        [Route("ObterTodos")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterTodos()
        {
            var us = User.Identity.Name;
            var isRole = User.IsInRole("Colaborador");

            _logger.LogInformation("Obtendo todas as contratações");
            return Ok(await _application.ObterTodosAsync());
        }

        [HttpGet]
        [Route("ObterDadosContratacaoCliente")]
        [ProducesResponseType(typeof(List<ApoliceDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterDadosContratacaoCliente()
        {
            _logger.LogInformation("Obtendo todas as contratações cliente e proposta");
            return Ok(await _application.ObterContratacaoPropostaClienteAsync());
        }

        [HttpGet]
        [Route("ObterTodosComFiltro")]
        [ProducesResponseType(typeof(List<ApoliceDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterTodosComFiltroAsync(DateTime? dataContratacao, string? numeroApolice, int status = -1)
        {
            var us = User.Identity.Name;
            var isRole = User.IsInRole("Colaborador");

            _logger.LogInformation("Obtendo todas as contratações com filto");
            return Ok(await _application.ObterTodosComFiltroAsync(dataContratacao, numeroApolice, status));
        }

        [HttpPost]
        [Route("Novo")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Novo([FromBody] ApoliceRequest request)
        {
            _logger.LogInformation("Adicionando nova contratação");
            return Ok(await _service.CriarApoliceAsync(request));
        }

        [HttpPut]
        [Route("Atualizar")]
        [ProducesResponseType(typeof(ApoliceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Atualizar([FromBody] ApoliceRequest request)
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
