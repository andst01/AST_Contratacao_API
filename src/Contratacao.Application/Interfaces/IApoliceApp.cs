using Contratacao.Application.DTO;
using Contratacao.Application.Request;
using Contratacao.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Application.Interfaces
{
    public interface IApoliceApp : IAppBase<Apolice, ApoliceRequest, ApoliceDTO>
    {
        Task<List<ApoliceDTO>> ObterContratacaoPropostaClienteAsync();

        Task<ApoliceDTO> ObterContratacaoPropostaClientePorIdAsync(int id);

        Task<List<ApoliceDTO>> ObterTodosComFiltroAsync(DateTime? dataContratacao, string? numeroApolice, int status);
    }
}
