using AutoMapper;
using BusinessLayer.ReponseModel;
using BusinessLayer.ConfigHelper;

using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.CategoryBusiness
{
    public class ViroCureUserService : IViroCureUserService
    {
        private readonly IConfiguration _configuration;
        private UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ViroCureUserService(UnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork ??= unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<BaseResponseModel<LoginReponseModel>> Login(string email, string password)
        {
            try
            {
                var User = await _unitOfWork.ViroCureUserRepository.LoginUser(email, password);
                if (User == null)
                {
                    return new BaseResponseModel<LoginReponseModel>()
                    {
                        Code = 401,
                        Message = "Unauthorized (for incorrect email/password)",
                        Data = null
                    };
                }

                string token = ConfigHelper.Helper.GenerateJwtToken(User, _configuration);
                return new BaseResponseModel<LoginReponseModel>()
                {
                    Code = 200,
                    Message = "Login Success",
                    Data = new LoginReponseModel
                    {
                        Token = new TokenModel() { Token = token },
                        User = _mapper.Map<UserReponseModel>(User)
                    }
                };
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý ngoại lệ
                return new BaseResponseModel<LoginReponseModel>()
                {
                    Code = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}
