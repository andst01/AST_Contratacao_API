using AutoMapper;
using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces;
using Contratacao.Application.Request;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Interfaces;

namespace Contratacao.Application
{
    public class ApoliceApp : AppBase<Apolice, ApoliceRequest, ApoliceDTO>, IApoliceApp
    {
        private readonly IApoliceRepoitorio _apoliceRepoitorio;
        public ApoliceApp(IApoliceRepoitorio apoliceRepoitorio,
                          IMapper mapper) : base(apoliceRepoitorio, mapper)
        {
            _apoliceRepoitorio = apoliceRepoitorio;
        }

        public async Task<List<ApoliceDTO>> ObterContratacaoPropostaClienteAsync()
        {
            var retorno = await _apoliceRepoitorio.ObterContratacaoPropostaClienteAsync();
            return _mapper.Map<List<ApoliceDTO>>(retorno);
        }

        public async Task<List<ApoliceDTO>> ObterTodosComFiltroAsync(DateTime? dataContratacao, string? numeroApolice, int status)
        {
            // var request = _mapper.Map<Apolice>(apoliceDTO);
            var retorno = await _apoliceRepoitorio.ObterTodosComFiltroAsync(dataContratacao, numeroApolice, status);

            return _mapper.Map<List<ApoliceDTO>>(retorno);
        }

        public async Task<ApoliceDTO> ObterContratacaoPropostaClientePorIdAsync(int id)
        {
            var retorno = await _apoliceRepoitorio.ObterContratacaoPropostaClientePorIdAsync(id);
            return _mapper.Map<ApoliceDTO>(retorno);
        }

    }
}
