using AutoMapper;
using Contratacao.Application.Interfaces;
using Contratacao.Domain.Interfaces;

namespace Contratacao.Application
{
    public class AppBase<TEntity, TDto> : IAppBase<TEntity, TDto>
       where TEntity : class
       where TDto : class
    {

        protected readonly IRepositorioBase<TEntity> _repositorio;
        protected readonly IMapper _mapper;

        public AppBase(IRepositorioBase<TEntity> repositorio,
                       IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }
        public async Task<TDto> AdicionarAsync(TDto viewModel)
        {

            var entity = _mapper.Map<TEntity>(viewModel);

            var retorno = await _repositorio.AdicionarAsync(entity);

            await _repositorio.SaveChangesAsync();

            return _mapper.Map<TDto>(retorno);
        }

      

        public async Task<TDto> AtualizarAsync(TDto viewModel, object id)
        {
            var entity = _mapper.Map<TEntity>(viewModel);

            var retorno = await _repositorio.AtualizarAsync(entity, id);

            await _repositorio.SaveChangesAsync();

            return _mapper.Map<TDto>(retorno);
        }

        public async Task<int> ExcluirAsync(int id)
        {
            await _repositorio.ExcluirAsync(id);

            return await _repositorio.SaveChangesAsync();
        }


        public async Task<TDto> ObterPorIdAssyn(int id)
        {
            var retorno = await _repositorio.ObterPorIdAsync(id);

            return _mapper.Map<TDto>(retorno);

        }

        public async Task<List<TDto>> ObterTodosAsync()
        {
            var retorno = await _repositorio.ObterTodosAsync();

            return _mapper.Map<List<TDto>>(retorno);
        }
    }
}
