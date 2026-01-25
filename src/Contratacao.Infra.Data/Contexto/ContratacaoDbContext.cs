using Contratacao.Domain.Entidades;
using Contratacao.Infra.Data.Mapeamento;
using Microsoft.EntityFrameworkCore;

namespace Contratacao.Infra.Data.Contexto
{
    public class ContratacaoDbContext : DbContext
    {
        public DbSet<Apolice> Apolices { get; set; }

        public ContratacaoDbContext(DbContextOptions<ContratacaoDbContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApoliceMap());

            base.OnModelCreating(modelBuilder);
            
        }
    }
}
