using GEntities.Entity;
using LarEntities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarEntities.ParameterDTO
{
    public class PessoaParamDTO
    {
        public int? IdPessoa {  get; set; }
        public string? CPF { get; set; }
        public string? Nome {  get; set; }   
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public List<TelefoneParamDTO> Telefones { get; set; }
    }

}
