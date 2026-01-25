using Contratacao.Domain.Interfaces;
using Contratacao.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Contratacao.Infra.Data.Repositorio
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        protected readonly ContratacaoDbContext _context;

        public RepositorioBase(ContratacaoDbContext context)
        {
            _context = context;
        }

        public async Task<T> AdicionarAsync(T entity)
        {
            var retorno = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return retorno.Entity;
        }

       
        public async Task<T> AtualizarAsync(T entity, object id)
        {

            var trackedEntity = _context.ChangeTracker
                               .Entries<T>()
                               .FirstOrDefault(e => e.Entity.Equals(entity));

            if (trackedEntity != null)
            {
               
                await _context.SaveChangesAsync();
                return trackedEntity.Entity;
            }

            var existingEntity = _context.Set<T>().Find(id);

            if (existingEntity == null) throw new Exception("A entidade não foi encontrada");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();

            var retorno = _context.Set<T>().Find(id);

            return retorno;
        }

        public async Task<int> ExcluirAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> ObterPorFiltroAsync(Expression<Func<T, bool>> filter = null,
                                                             Func<IQueryable<T>, IQueryable<T>> include = null,
                                                             bool asNoTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (asNoTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<T> ObterPorIdAssyn(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ObterTodosAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
