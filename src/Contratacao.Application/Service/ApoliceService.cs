using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces.Map;
using Contratacao.Application.Interfaces.Service;
using Contratacao.Application.Request;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Enums;
using Contratacao.Domain.Interfaces;

namespace Contratacao.Application.Service
{
    
    public class ApoliceService : IApoliceService
    {
        private readonly IApoliceRepoitorio _apoliceRepoitorio;
        private readonly IMapBase<Apolice, ApoliceRequest> _mapRequestToEntity;
        private readonly IMapBase<ApoliceDTO, Apolice> _mapEntityToDTO;
        private readonly IRepositorioBase<Proposta> _propostaRepositorio;

        public ApoliceService(IApoliceRepoitorio apoliceRepoitorio,
                                  IRepositorioBase<Proposta> propostaRepositorio,
                                  IMapBase<Apolice, ApoliceRequest> mapRequestToEntity,
                                  IMapBase<ApoliceDTO, Apolice> mapEntityToDTO)
        {
            _apoliceRepoitorio = apoliceRepoitorio;
            _propostaRepositorio = propostaRepositorio;
            _mapRequestToEntity = mapRequestToEntity;
            _mapEntityToDTO = mapEntityToDTO;
        }

        public async Task<ApoliceDTO> CriarApoliceAsync(ApoliceRequest request)
        {
            var retorno = new ApoliceDTO();
            retorno.Mensagem = new();
            //var apolice = _mapper.Map<Apolice>(request);
            var apolice = _mapRequestToEntity.Map(request);
            var proposta = await _propostaRepositorio.ObterPorIdAsync(apolice.IdProposta);
           
            if (proposta == null)
            {
                retorno.Mensagem.Sucesso = false;
                retorno.Mensagem.Descricao = "Proposta não encontrada.";
                return retorno;
            }
            else if (proposta.Status != EnumStatusProposta.Aprovada)
            {
                retorno.Mensagem.Sucesso = false;
                retorno.Mensagem.Descricao = "A proposta deve estar aprovada para criar uma apólice";
                return retorno;

            }

            var resultado = await _apoliceRepoitorio.AdicionarAsync(apolice);
            
            await _apoliceRepoitorio.SaveChangesAsync();

           // retorno = _mapper.Map<ApoliceDTO>(resultado);
            retorno = _mapEntityToDTO.Map(resultado);
            retorno.Mensagem = new();
            retorno.Mensagem.Sucesso = true;
            retorno.Mensagem.Descricao = "Apólice criada com sucesso.";

            return retorno;
        }
    }
}
