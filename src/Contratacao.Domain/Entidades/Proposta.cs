using Contratacao.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Domain.Entidades
{
    [ExcludeFromCodeCoverage]
    public class Proposta
    {
        public int Id { get; set; }
        public string NumeroProposta { get; set; }
        public string TipoSeguro { get; set; }

        /// <summary>
        /// Status da proposta
        /// Criada = 0,
        /// EmAnalise = 1,
        /// Aprovada = 2,
        /// Recusada = 3,
        /// Expirada = 4
        /// </summary>
        public EnumStatusProposta Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public decimal Premio { get; set; }
        public decimal ValorCobertura { get; set; }
        public string FormaPagamento { get; set; }
        public int? QuantidadeParcelas { get; set; }
        public string CanalVenda { get; set; }
        public string Observacoes { get; set; }

        public int IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Apolice Apolice { get; set; }
    }
}
