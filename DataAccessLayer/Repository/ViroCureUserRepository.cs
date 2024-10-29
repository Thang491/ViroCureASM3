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
    public class ViroCureUserRepository: GenericRepo<ViroCureUser>
    {

        public ViroCureUserRepository() { }
        public async Task<ViroCureUser> LoginUser(string email,string password)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.Email == email && t.Password == password);
        }
    }
}
