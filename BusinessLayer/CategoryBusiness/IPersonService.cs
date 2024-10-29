using BusinessLayer.ReponseModel;
using BusinessLayer.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.CategoryBusiness
{
    public interface IPersonService
    {
        Task<BaseResponseModel<PersonReponseModel>> createPerson(PersonRequestModel request);
        Task<BaseResponseModel<getPersonReponseModel>> getPersonById(int id);
        Task<BaseResponseModel<List<getPersonReponseModel>>> getAllPerson();
        Task<BaseResponseModel> deletePersonbyId(int id);
        Task<BaseResponseModel> UpdatePerson(int id, UpdatePersonRequestmodel request);
    }
}
