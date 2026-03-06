using Contratacao.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Domain.Interfaces
{
    public interface IApoliceRepoitorio : IRepositorioBase<Apolice>
    {
        Task<List<Apolice>> ObterContratacaoPropostaClienteAsync();

        Task<Apolice> ObterContratacaoPropostaClientePorIdAsync(int id);

        Task<List<Apolice>> ObterTodosComFiltroAsync(DateTime? dataContratacao, string? numeroApolice, int status);
    }
}
