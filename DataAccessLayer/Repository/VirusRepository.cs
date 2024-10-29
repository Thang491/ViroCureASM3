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
    public class VirusRepository:GenericRepo<Virus>
    {
        public VirusRepository() { }
        public async Task<Virus> findVirusbyName(string name)
        {
             return  await _dbSet.FirstOrDefaultAsync(m => m.VirusName == name);
        }
        

    }
}
