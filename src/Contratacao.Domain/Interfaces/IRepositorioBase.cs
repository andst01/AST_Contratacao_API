using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Domain.Interfaces
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<T> AdicionarAsync(T entity);

        Task<T> AtualizarAsync(T entity, object id);

        Task<int> ExcluirAsync(int id);

        Task<List<T>> ObterTodosAsync();

        Task<T> ObterPorIdAssyn(int id);

        Task<IEnumerable<T>> ObterPorFiltroAsync(Expression<Func<T, bool>> filter = null,
                                      Func<IQueryable<T>, IQueryable<T>> include = null,
                                      bool asNoTracking = true);
    }
}
