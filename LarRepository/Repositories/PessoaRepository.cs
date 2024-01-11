using GEntities.Entity;
using LarContracts.IRepository;
using LarEntities.Entity;
using LarRepository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarRepository.Repositories
{
    public class PessoaRepository : BaseRepository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(LarEFContext context) : base(context) { }

        public List<Pessoa> GetAllPessoa()
        {
            return _context.Set<Pessoa>()
               .Include(c => c.Telefone)
                .ToList();
        }

    }
}
