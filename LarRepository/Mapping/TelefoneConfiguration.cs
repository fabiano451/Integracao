using GEntities.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LarEntities.Entity;

namespace LarRepository.Mapping
{
    public class TelefoneConfiguration : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable("Telefone");

            builder.HasKey(p => p.IdTelefone).HasName("PK_Telefone");

            builder.Property(t => t.IdTelefone).HasColumnName("IdTelefone").HasColumnType("int").IsRequired();
            builder.Property(p => p.Tipo).HasColumnName("Tipo").HasColumnType("char(12)");
            builder.Property(p => p.NumeroTelefone).HasColumnName("NumeroTelefone").HasColumnType("nchar(11)").IsRequired();
            builder.HasOne(t => t.Pessoa).WithMany(p => p.Telefone).HasForeignKey(ncc => ncc.IdPessoa);
           
        }
    }
}
