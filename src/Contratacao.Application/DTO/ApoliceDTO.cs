using Contratacao.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Application.DTO
{
    public class ApoliceDTO
    {
        public int Id { get; set; }
        public string NumeroApolice { get; set; }
        public Guid PropostaId { get; set; }
        public EnumStatusProposta Status { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFimVigencia { get; set; }
        public decimal PremioFinal { get; set; }
        public decimal ValorCobertura { get; set; }
        public string FormaPagamento { get; set; }
        public int QuantidadeParcelas { get; set; }
        public DateTime DataContratacao { get; set; }
    }
}
