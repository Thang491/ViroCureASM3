using BusinessLayer.ReponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.CategoryBusiness
{
    public interface IViroCureUserService
    {
        Task<BaseResponseModel<LoginReponseModel>> Login(string email, string password);
    }
}
