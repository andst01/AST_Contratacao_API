using Contratacao.Application.DTO;
using Contratacao.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Application.Interfaces
{
    public interface IApoliceApp : IAppBase<Apolice, ApoliceDTO>
    {
        Task<List<ApoliceDTO>> ObterDadosContratacaoClienteAsync();
    }
}
