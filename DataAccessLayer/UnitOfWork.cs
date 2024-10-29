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
        private PersonRepository _personRepo;

        private PersonVirusRepository _personVirusRepo;

        private VirusRepository _virusRepo;

        public UnitOfWork()
        {
        }
        public ViroCureUserRepository ViroCureUserRepository
        {
            get { return _virocureuser ??= new Repository.ViroCureUserRepository(); }
        }
        public PersonRepository PersonRepository
        {
            get { return _personRepo ??= new Repository.PersonRepository(); }
        }
        public PersonVirusRepository PersonVirusRepository
        {
            get { return _personVirusRepo ??= new Repository.PersonVirusRepository(); }
        }
        public VirusRepository VirusRepository
        {
            get { return _virusRepo ??= new Repository.VirusRepository(); }
        }
    }
}
