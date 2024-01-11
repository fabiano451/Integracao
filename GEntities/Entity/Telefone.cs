using GEntities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarEntities.Entity
{
    public class Telefone
    {
        
        public int IdTelefone { get; set; }
        
        public string Tipo {  get; set; }  

        public string NumeroTelefone { get; set; }

        public int IdPessoa { get; set; }

        public Pessoa Pessoa { get; set; }

    }
}
