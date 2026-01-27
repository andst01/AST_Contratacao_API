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

        public async Task<List<ApoliceDTO>> ObterDadosContratacaoClienteAsync()
        {
            var retorno = await _apoliceRepoitorio.ObterDadosContratacaoClienteAsync();
            return _mapper.Map<List<ApoliceDTO>>(retorno);
        }
    }
}
