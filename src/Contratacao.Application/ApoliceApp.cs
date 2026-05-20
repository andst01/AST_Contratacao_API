using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces;
using Contratacao.Application.Interfaces.Map;
using Contratacao.Application.Request;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Interfaces;

namespace Contratacao.Application
{
    public class ApoliceApp 
        : AppBase<Apolice, 
                  ApoliceRequest, 
                  ApoliceDTO>, IApoliceApp
    {
        private readonly IApoliceRepoitorio _apoliceRepoitorio;
        private readonly IMapBase<ApoliceDTO, Apolice> _mapEntityToDto;
        public ApoliceApp(IApoliceRepoitorio apoliceRepoitorio,
                          IMapBase<Apolice, ApoliceRequest> mapRequestToEntity,
                          IMapBase<ApoliceDTO, Apolice> mapEntityToDto) 
            : base(apoliceRepoitorio, mapRequestToEntity, mapEntityToDto)
        {
            _apoliceRepoitorio = apoliceRepoitorio;
            _mapEntityToDto = mapEntityToDto;
        }

        public async Task<List<ApoliceDTO>> ObterContratacaoPropostaClienteAsync()
        {
            //var retorno = await _apoliceRepoitorio.ObterContratacaoPropostaClienteAsync();

            //return _mapper.Map<List<ApoliceDTO>>(retorno);

            var result = await _apoliceRepoitorio.ObterContratacaoPropostaClienteAsync();
            var restorno = result.Select(x => _mapEntityToDto.Map(x)).ToList();
            return restorno;
        }

        public async Task<List<ApoliceDTO>> ObterTodosComFiltroAsync(DateTime? dataContratacao, string? numeroApolice, int status)
        {
            // var request = _mapper.Map<Apolice>(apoliceDTO);
            //var retorno = await _apoliceRepoitorio.ObterTodosComFiltroAsync(dataContratacao, numeroApolice, status);

            //return _mapper.Map<List<ApoliceDTO>>(retorno);

            var result = await _apoliceRepoitorio.ObterTodosComFiltroAsync(dataContratacao, numeroApolice, status);
            var restorno = result.Select(x => _mapEntityToDto.Map(x)).ToList();
            return restorno;
        }

        public async Task<ApoliceDTO> ObterContratacaoPropostaClientePorIdAsync(int id)
        {
            //var retorno = await _apoliceRepoitorio.ObterContratacaoPropostaClientePorIdAsync(id);
            //return _mapper.Map<ApoliceDTO>(retorno);

            var result = await _apoliceRepoitorio.ObterContratacaoPropostaClientePorIdAsync(id);
            var restorno = _mapEntityToDto.Map(result);
            return restorno;
        }

    }
}
