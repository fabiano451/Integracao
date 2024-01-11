using LarEntities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarEntities.DTO
{
    public class PessoaDTO
    {
        public int IdPessoa { get; set; }
        public string? CPF { get; set; }
        public string? Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public List<Telefone>? Telefone { get; set; }
    }
}
