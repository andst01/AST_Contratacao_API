using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces.Map;
using Contratacao.Domain.Entidades;


namespace Contratacao.Application.Map
{
    public class ApoliceEntityToDTO : IMapBase<ApoliceDTO, Apolice>
    {
        private readonly IMapBase<PropostaDTO, Proposta> _mapPropostaEntityToDTo;

        public ApoliceEntityToDTO(IMapBase<PropostaDTO,Proposta> mapPropostaEntityToDTo)
        {
            _mapPropostaEntityToDTo = mapPropostaEntityToDTo;
        }

        public ApoliceDTO Map(Apolice source)
        {
            if (source == null)
                return null;

            var apoliceDTO = new ApoliceDTO
            {
                Id = source.Id,
                NumeroApolice = source.NumeroApolice,
                IdProposta = source.IdProposta,
                DataInicioVigencia = source.DataInicioVigencia,
                DataFimVigencia = source.DataFimVigencia,
                PremioFinal = source.PremioFinal,
                ValorCobertura = source.ValorCobertura,
                FormaPagamento = source.FormaPagamento,
                QuantidadeParcelas = source.QuantidadeParcelas,
                DataContratacao = source.DataContratacao,
                CodigoStatus = (int)source.Status,
                Proposta = source.Proposta == null 
                            ? null 
                            : _mapPropostaEntityToDTo.Map(source.Proposta),
            };

            return apoliceDTO;
        }
    }
}
