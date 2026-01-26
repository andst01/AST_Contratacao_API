using Contratacao.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Infra.Data.Mapeamento
{
    public class ApoliceMap : IEntityTypeConfiguration<Apolice>
    {
        public void Configure(EntityTypeBuilder<Apolice> builder)
        {
            builder.ToTable("Apolices");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.NumeroApolice).IsRequired().HasMaxLength(50);
            builder.Property(a => a.IdProposta).IsRequired();
            builder.Property(a => a.Status).IsRequired();
            builder.Property(a => a.DataInicioVigencia).IsRequired();
            builder.Property(a => a.DataFimVigencia).IsRequired(false);
            builder.Property(a => a.PremioFinal).IsRequired(false).HasColumnType("decimal(18,2)");
            builder.Property(a => a.ValorCobertura).IsRequired(false).HasColumnType("decimal(18,2)");
            builder.Property(a => a.FormaPagamento).IsRequired().HasMaxLength(50);
            builder.Property(a => a.QuantidadeParcelas).IsRequired(false);
            builder.Property(a => a.DataContratacao).IsRequired();
            builder.HasOne(a => a.Proposta)
                   .WithOne(p => p.Apolice)
                   .HasForeignKey<Apolice>(a => a.IdProposta)
                   .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
