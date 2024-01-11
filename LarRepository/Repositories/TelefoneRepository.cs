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
    public class TelefoneRepository : BaseRepository<Telefone>, ITelefoneRepository
    {
        public TelefoneRepository(LarEFContext context) : base(context) { }

        public List<Telefone> GetAllTelefone()
        {
            return _context.Set<Telefone>().ToList();
        }
    }
}
