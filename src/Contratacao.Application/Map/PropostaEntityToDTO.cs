using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces.Map;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Application.Map
{
    public class PropostaEntityToDTO : IMapBase<PropostaDTO, Proposta>
    {
        private readonly IMapBase<ClienteDTO, Cliente> _clienteMapper;

        public PropostaEntityToDTO(IMapBase<ClienteDTO, Cliente> clienteMapper)
        {
            _clienteMapper = clienteMapper;
        }
        public PropostaDTO Map(Proposta source)
        {
           if(source == null) throw new ArgumentNullException(nameof(source));

           var propostaDTO = new PropostaDTO
           {
               Id = source.Id,
               NumeroProposta = source.NumeroProposta,
               TipoSeguro = source.TipoSeguro,
               Status = (int)source.Status,
               DataCriacao = source.DataCriacao,
               DataValidade = source.DataValidade,
               Premio = source.Premio,
               ValorCobertura = source.ValorCobertura,
               FormaPagamento = source.FormaPagamento,
               QuantidadeParcelas = source.QuantidadeParcelas,
               CanalVenda = source.CanalVenda,
               Observacoes = source.Observacoes,
               IdCliente = source.IdCliente,
               Cliente = source.Cliente == null 
                           ? null 
                           : _clienteMapper.Map(source.Cliente),
           };
           
            return propostaDTO;
        }
    }
}
