using AutoMapper;
using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces.Service;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Enums;
using Contratacao.Domain.Interfaces;

namespace Contratacao.Application.Service
{
    
    public class ApoliceService : IApoliceService
    {
        private readonly IApoliceRepoitorio _apoliceRepoitorio;
        private readonly IMapper _mapper;
        private readonly IRepositorioBase<Proposta> _propostaRepositorio;

        public ApoliceService(IApoliceRepoitorio apoliceRepoitorio,
                                  IRepositorioBase<Proposta> propostaRepositorio,
                                  IMapper mapper)
        {
            _apoliceRepoitorio = apoliceRepoitorio;
            _propostaRepositorio = propostaRepositorio;
            _mapper = mapper;
        }

        public async Task<ApoliceDTO> CriarApoliceAsync(ApoliceDTO request)
        {
            var apolice = _mapper.Map<Apolice>(request);
            var proposta = await _propostaRepositorio.ObterPorIdAsync(apolice.IdProposta);
           
            if (proposta == null)
            {
                throw new Exception("Proposta não encontrada.");
            }
            else if (proposta.Status != EnumStatusProposta.Aprovada)
            {
                throw new Exception("A proposta deve estar aprovada para criar uma apólice.");
            }

            var resultado = await _apoliceRepoitorio.AdicionarAsync(apolice);
            
            await _apoliceRepoitorio.SaveChangesAsync();

            var retorno = _mapper.Map<ApoliceDTO>(resultado);
            
            return retorno;
        }
    }
}
