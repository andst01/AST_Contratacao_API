using Contratacao.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Domain.Entidades
{
    public class Apolice
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

        //public ContratacaoCliente Cliente { get; set; }
        //public List<Parcela> Parcelas { get; set; }
    }
}
