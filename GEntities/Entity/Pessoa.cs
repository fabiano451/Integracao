using LarEntities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEntities.Entity
{
    public class Pessoa
    {
       
        public int IdPessoa { get; set; }
        public string? CPF { get; set; }
        public string Nome {  get; set; }   
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public List<Telefone>? Telefone { get; set; }
    }
}
