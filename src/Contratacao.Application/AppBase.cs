using AutoMapper;
using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces;
using Contratacao.Domain.Interfaces;

namespace Contratacao.Application
{
    public class AppBase<TEntity, TRequest, TDto> 
        : IAppBase<TEntity, TRequest, TDto>
       where TEntity : class
       where TRequest : class
       where TDto : BaseDTO
    {

        protected readonly IRepositorioBase<TEntity> _repositorio;
        protected readonly IMapper _mapper;

        public AppBase(IRepositorioBase<TEntity> repositorio,
                       IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }
        public async Task<TDto> AdicionarAsync(TRequest request)
        {

            var entity = _mapper.Map<TEntity>(request);

            var resultado = await _repositorio.AdicionarAsync(entity);

            await _repositorio.SaveChangesAsync();

            var retorno = _mapper.Map<TDto>(resultado);

            retorno.Mensagem = new();
            retorno.Mensagem.Sucesso = true;
            retorno.Mensagem.Descricao = "Registro adicionado com sucesso.";

            return retorno;
        }

      

        public async Task<TDto> AtualizarAsync(TRequest request, object id)
        {
            var entity = _mapper.Map<TEntity>(request);

            var resultado = await _repositorio.AtualizarAsync(entity, id);

            await _repositorio.SaveChangesAsync();

            var retorno = _mapper.Map<TDto>(resultado);

            retorno.Mensagem = new();
            retorno.Mensagem.Sucesso = true;
            retorno.Mensagem.Descricao = "Registro atualizado com sucesso.";

            return retorno;
        }

        public async Task<BaseDTO> ExcluirAsync(int id)
        {
            var retono = new BaseDTO();
            await _repositorio.ExcluirAsync(id);

            var resultado = await _repositorio.SaveChangesAsync();
            if (resultado > 0)
            {
                retono.Mensagem = new();
                retono.Mensagem.Sucesso = true;
                retono.Mensagem.Descricao = "Registro excluído com sucesso.";
            }
            else
            {
                retono.Mensagem = new();
                retono.Mensagem.Sucesso = false;
                retono.Mensagem.Descricao = "Não foi possível excluir o registro.";
            }

            return retono;
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
