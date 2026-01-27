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
            return retorno.Entity;
        }

       
        public async Task<T> AtualizarAsync(T entity, object id)
        {

            var set = _context.Set<T>();

            var existingEntity = await set.FindAsync(id);

            if (existingEntity == null)
                throw new Exception("A entidade não foi encontrada");

            // Copia os valores para a entidade rastreada
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            // Garante que o estado é Modified
            _context.Entry(existingEntity).State = EntityState.Modified;

            return existingEntity;
        }

        public async Task ExcluirAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);

            _context.Entry(entity).State = EntityState.Deleted;

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

        public async Task<T> ObterPorIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ObterTodosAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
