using DataAccessLayer.Base;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class PersonVirusRepository : GenericRepo<PersonVirus>

    {
        public PersonVirusRepository() { }
        public async Task<IEnumerable<PersonVirus>> findVirusbyPersonId(int id)
        {
            return await _dbSet
       .Include(pv => pv.Virus) // Tải Virus
       .Include(pv => pv.Person) // Tải Person (nếu cần thiết)
       .Where(m => m.PersonId == id)
       .ToListAsync();
        }
    }
}
