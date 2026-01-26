using Contratacao.Domain.Entidades;
using Contratacao.Domain.Interfaces;
using Contratacao.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Contratacao.Infra.Data.Repositorio
{
    public class ApoliceRepositorio : RepositorioBase<Apolice>, IApoliceRepoitorio
    {
        public ApoliceRepositorio(ContratacaoDbContext context) : base(context)
        {
        }

        public async Task<List<Apolice>> ObterDadosContratacaoClienteAsync()
        {
             var retorno = await _context.Set<Apolice>()
                                   .Include(a => a.Proposta)
                                   .ThenInclude(p => p.Cliente)
                                   .ToListAsync();
            return retorno;
        }
    }
}
