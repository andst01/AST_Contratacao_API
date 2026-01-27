using Contratacao.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Application.Interfaces
{
    public interface IAppBase<TEntity, TRequest, TDto>
      where TEntity : class
      where TRequest : class
      where TDto : BaseDTO
    {
        Task<TDto> AdicionarAsync(TRequest entity);

        Task<TDto> AtualizarAsync(TRequest entity, object id);

        Task<BaseDTO> ExcluirAsync(int id);

        Task<List<TDto>> ObterTodosAsync();

        Task<TDto> ObterPorIdAssyn(int id);


    }
}
