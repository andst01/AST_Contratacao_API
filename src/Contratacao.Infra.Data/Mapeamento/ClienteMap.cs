using Contratacao.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contratacao.Infra.Data.Mapeamento
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            builder.Property(c => c.CpfCnpj).IsRequired().HasMaxLength(20);
            builder.Property(c => c.DataNascimento).IsRequired();
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Telefone).IsRequired().HasMaxLength(15);
            builder.Property(c => c.Endereco).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Cidade).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Estado).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Cep).IsRequired().HasMaxLength(10);


        }
    }
}
