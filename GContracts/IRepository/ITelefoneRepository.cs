using GEntities.Entity;
using LarEntities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarContracts.IRepository
{
    public interface ITelefoneRepository : IBaseRepository<Telefone>
    {
        List<Telefone> GetAllTelefone();
    }
}
      
