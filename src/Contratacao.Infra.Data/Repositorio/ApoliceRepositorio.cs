using Contratacao.Domain.Entidades;
using Contratacao.Domain.Interfaces;
using Contratacao.Infra.Data.Contexto;

namespace Contratacao.Infra.Data.Repositorio
{
    public class ApoliceRepositorio : RepositorioBase<Apolice>, IApoliceRepoitorio
    {
        public ApoliceRepositorio(ContratacaoDbContext context) : base(context)
        {
        }
    }
}
