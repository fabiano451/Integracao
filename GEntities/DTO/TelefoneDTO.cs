using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarEntities.DTO
{
    public class TelefoneDTO
    {
        public int IdTelefone { get; set; }

        public string? Tipo { get; set; }

        public string? NumeroTelefone { get; set; }

        public int IdPessoa { get; set; }

    }
}
