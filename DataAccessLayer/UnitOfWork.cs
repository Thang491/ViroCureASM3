using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UnitOfWork
    {
        private ViroCureUserRepository _virocureuser;
        public UnitOfWork()
        {
        }
        public ViroCureUserRepository ViroCureUserRepository
        {
            get { return _virocureuser ??= new Repository.ViroCureUserRepository(); }
        }
    }
}
