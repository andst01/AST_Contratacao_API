using Contratacao.Application.Interfaces.Map;
using Contratacao.Application.Request;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Application.Map
{
    public class ApoliceRequestToEntity : IMapBase<Apolice, ApoliceRequest>
    {
        public Apolice Map(ApoliceRequest source)
        {
            if (source == null)
                return null;

            var apolice = new Apolice
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
                Status = (EnumStatusApolice)source.CodigoStatus
            };

            return apolice;
        }
    }
}
