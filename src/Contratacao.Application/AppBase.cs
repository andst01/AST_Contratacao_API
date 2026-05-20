using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces;
using Contratacao.Application.Interfaces.Map;
using Contratacao.Domain.Interfaces;

namespace Contratacao.Application
{
    public class AppBase<TEntity, TRequest, TDto> 
        : IAppBase<TEntity, TRequest, TDto>
       where TEntity : class
       where TRequest : class
       where TDto : BaseDTO
    {

        protected readonly IMapBase<TEntity, TRequest> _mapRequestToEntity;
        protected readonly IMapBase<TDto, TEntity> _mapEntityToDto;
        protected readonly IRepositorioBase<TEntity> _repositorio;
       

        public AppBase(IRepositorioBase<TEntity> repositorio,
                       IMapBase<TEntity, TRequest> mapRequestToEntity,
                       IMapBase<TDto, TEntity> mapEntityToDto)
        {
            _repositorio = repositorio;
            _mapRequestToEntity = mapRequestToEntity;
            _mapEntityToDto = mapEntityToDto;
        }
        public async Task<TDto> AdicionarAsync(TRequest request)
        {
            //var entity = _mapper.Map<TEntity>(request);
            var entity = _mapRequestToEntity.Map(request);

            var resultado = await _repositorio.AdicionarAsync(entity);

            await _repositorio.SaveChangesAsync();

            //var retorno = _mapper.Map<TDto>(resultado);
            var retorno = _mapEntityToDto.Map(resultado);

            retorno.Mensagem = new();
            retorno.Mensagem.Sucesso = true;
            retorno.Mensagem.Descricao = "Registro adicionado com sucesso.";

            return retorno;
        }

      

        public async Task<TDto> AtualizarAsync(TRequest request, object id)
        {
            //var entity = _mapper.Map<TEntity>(request);
            var entity = _mapRequestToEntity.Map(request);

            var resultado = await _repositorio.AtualizarAsync(entity, id);

            await _repositorio.SaveChangesAsync();

            // var retorno = _mapper.Map<TDto>(resultado);
            var retorno = _mapEntityToDto.Map(resultado);

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

            // return _mapper.Map<TDto>(retorno);
            return _mapEntityToDto.Map(retorno);

        }

        public async Task<List<TDto>> ObterTodosAsync()
        {
            // return _mapper.Map<List<TDto>>(retorno);
            var result = await _repositorio.ObterTodosAsync();

            var retorno = result.Select(x => _mapEntityToDto.Map(x)).ToList();

            return retorno;
        }
    }
}
