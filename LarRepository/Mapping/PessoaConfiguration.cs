using GEntities.Entity;
using LarEntities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarRepository.Mapping
{

    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoa");

            builder.HasKey(p => p.IdPessoa).HasName("PK_Pessoa");

            builder.Property(p => p.IdPessoa).HasColumnName("IdPessoa").HasColumnType("int").IsRequired();
            builder.Property(p => p.CPF).HasColumnName("CPF").HasColumnType("nchar(11)").IsRequired();
            builder.Property(p => p.Nome).HasColumnName("Nome").HasColumnType("nchar(11)").IsRequired();
            builder.Property(p => p.DataNascimento).HasColumnName("DateNascimento").HasColumnType("datetime2(7)");
            builder.Property(p => p.Ativo).HasColumnName("Ativo").HasColumnType("bit");
        }
    }
}
