using Contratacao.Domain.Entidades;
using Contratacao.Domain.Enums;
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

        public async Task<List<Apolice>> ObterContratacaoPropostaClienteAsync()
        {
             var retorno = await _context.Set<Apolice>()
                                   .Include(a => a.Proposta)
                                   .ThenInclude(p => p.Cliente)
                                   .ToListAsync();
            return retorno;
        }

        public async Task<List<Apolice>> ObterTodosComFiltroAsync(DateTime? dataContratacao, string? numeroApolice, int status)
        {
            var retorno =  _context.Set<Apolice>()
                                  .Include(a => a.Proposta)
                                  .ThenInclude(p => p.Cliente)
                                  .AsQueryable();

            if (dataContratacao.HasValue && dataContratacao.Value != DateTime.MinValue)
                retorno = retorno.Where(x => x.DataContratacao.Date >= dataContratacao);
            if(status >= 0)
                retorno = retorno.Where(x => x.Status == (EnumStatusApolice)status);
            if(!string.IsNullOrEmpty(numeroApolice))
                retorno = retorno.Where(x => x.NumeroApolice.Contains(numeroApolice));
            
            return await retorno.ToListAsync();

            
        }

        // ObterTodosComFiltrosAsync



        public async Task<Apolice> ObterContratacaoPropostaClientePorIdAsync(int id)
        {
            var retorno = await _context.Set<Apolice>()
                                  .Include(a => a.Proposta)
                                  .ThenInclude(p => p.Cliente)
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync();
            return retorno;
        }
    }
}
