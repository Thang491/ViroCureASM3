using AutoMapper;
using BusinessLayer.ReponseModel;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLayer.Helper
{
    public class Mapper : Profile
    {

        public Mapper()
        {        
            CreateMap<ViroCureUser, UserReponseModel>();
            CreateMap<Person, getPersonReponseModel>();
        }

    }
}
