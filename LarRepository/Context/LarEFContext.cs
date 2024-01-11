using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LarRepository.Mapping;
using GEntities.Entity;
using System.Reflection.Emit;
using LarEntities.Entity;

namespace LarRepository.Context
{

    public class LarEFContext : DbContext
    {
        public LarEFContext(DbContextOptions<LarEFContext> options) : base(options)
        {
        }

        public LarEFContext()
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        
        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PessoaConfiguration());
            modelBuilder.ApplyConfiguration(new TelefoneConfiguration());
        }
    }
}

