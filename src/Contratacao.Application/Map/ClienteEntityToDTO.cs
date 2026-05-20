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
    public class ClienteEntityToDTO : IMapBase<ClienteDTO, Cliente>
    {
        public ClienteDTO Map(Cliente source)
        {
            if(source == null) throw new ArgumentNullException("source");

            var clienteDTO = new ClienteDTO
            {
                Id = source.Id,
                Nome = source.Nome,
                CpfCnpj = source.CpfCnpj,
                DataNascimento = source.DataNascimento,
                Email = source.Email,
                Telefone = source.Telefone,
                Endereco = source.Endereco,
                Cidade = source.Cidade,
                Estado = source.Estado,
                Cep = source.Cep,
               
            };

            return clienteDTO;
        }
    }
}
