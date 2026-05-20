using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces.Map;
using Contratacao.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Application.Map
{
    public class ApoliceEntityToDTO : IMapBase<ApoliceDTO, Apolice>
    {
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
                CodigoStatus = (int)source.Status
            };

            return apoliceDTO;
        }
    }
}
