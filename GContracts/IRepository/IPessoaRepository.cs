using GEntities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarContracts.IRepository
{
    public interface IPessoaRepository : IBaseRepository<Pessoa>
    {
        List<Pessoa> GetAllPessoa();
    }
}
