using AutoMapper;
using BusinessLayer.ReponseModel;
using BusinessLayer.RequestModel;
using DataAccessLayer;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.CategoryBusiness
{
    public class PersonService : IPersonService
    {
        private UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PersonService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponseModel<PersonReponseModel>> createPerson(PersonRequestModel request)
        {
            var existsPerson = await _unitOfWork.PersonRepository.GetByIdAsync(request.PersonId);
            if (existsPerson != null)
            {
                return new BaseResponseModel<PersonReponseModel>
                {
                    Code = 500,
                    Message = $"Person id already.",
                    Data = null
                };
            }
            try
            {
                Person person = new Person();
                Random rand = new Random();
                person.PersonId = request.PersonId;
                person.Fullname = request.Fullname;
                person.BirthDay = request.BirthDay;
                person.Phone = request.Phone;
                // Hoàn tất và thêm Person vào cơ sở dữ liệu
                await _unitOfWork.PersonRepository.CreateAsync(person);
                // Kiểm tra nếu danh sách Viruses không rỗng
                if (request.viruses != null && request.viruses.Count > 0)
                {
                    foreach (var virusInfo in request.viruses)
                    {
                        // Tìm virus trong database dựa vào tên
                        var virus = await _unitOfWork.VirusRepository.findVirusbyName(virusInfo.virusName);
                        if (virus != null)
                        {
                            PersonVirus personVirus = new PersonVirus
                            {
                                PersonId = person.PersonId,
                                VirusId = virus.VirusId,
                                ResistanceRate = virusInfo.resistanceRate
                            };

                            // Thêm từng virus cho Person
                            try
                            {
                                // Thêm từng virus cho Person
                                await _unitOfWork.PersonVirusRepository.CreateAsync(personVirus);
                            }
                            catch (Exception innerEx)
                            {
                                // Xử lý lỗi khi lưu PersonVirus
                                return new BaseResponseModel<PersonReponseModel>
                                {
                                    Code = 500,
                                    Message = $"An error occurred while saving the virus information: {innerEx.Message}.",
                                    Data = null
                                };
                            }
                        }
                        else
                        {
                            // Xử lý khi virus không tìm thấy (nếu cần)
                        }
                    }
                }
                return new BaseResponseModel<PersonReponseModel>
                {
                    Code = 200,
                    Message = "Person and viruses added successfully",
                    Data = new PersonReponseModel
                    {
                        personId = person.PersonId,

                    }
                };
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về lỗi
                return new BaseResponseModel<PersonReponseModel>
                {
                    Code = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponseModel<getPersonReponseModel>> getPersonById(int id)
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(id);
            if (person == null)
            {
                return new BaseResponseModel<getPersonReponseModel>
                {
                    Code = 404,
                    Message = "Person not found",
                    Data = null
                };
            }
            var personViruses = await _unitOfWork.PersonVirusRepository.findVirusbyPersonId(id);
            var viruses = personViruses
       .Where(pv => pv.Virus != null) // Kiểm tra Virus không null
       .Select(pv => new personVirus1
       {
           virusName = pv.Virus.VirusName, // Lấy tên virus
           resistanceRate = pv.ResistanceRate ?? 0 // Sử dụng giá trị mặc định nếu ResistanceRate null
       })
       .ToList();
            return new BaseResponseModel<getPersonReponseModel>
            {
                Code = 200,
                Message = "Person and viruses added successfully",
                Data = new getPersonReponseModel
                {
                    PersonId = person.PersonId,
                    Fullname = person.Fullname,
                    BirthDay = person.BirthDay,
                    Phone = person.Phone,
                    viruses = viruses
                }
            };
        }

        public async Task<BaseResponseModel<List<getPersonReponseModel>>> getAllPerson()
        {

            var persons = await _unitOfWork.PersonRepository.GetAllAsync();
            if (persons == null)
            {
                return new BaseResponseModel<List<getPersonReponseModel>>
                {
                    Code = 404,
                    Message = "Person null",
                    Data = null
                };
            }
            var personResponses = new List<getPersonReponseModel>();

            // Lặp qua từng người và lấy thông tin virus
            foreach (var person in persons)
            {
                // Lấy thông tin virus cho từng người
                var personViruses = await _unitOfWork.PersonVirusRepository.findVirusbyPersonId(person.PersonId);
                var viruses = personViruses
                    .Where(pv => pv.Virus != null) // Kiểm tra Virus không null
                    .Select(pv => new personVirus1
                    {
                        virusName = pv.Virus.VirusName, // Lấy tên virus
                        resistanceRate = pv.ResistanceRate ?? 0 // Sử dụng giá trị mặc định nếu ResistanceRate null
                    })
                    .ToList();

                // Tạo đối tượng response cho từng người
                personResponses.Add(new getPersonReponseModel
                {
                    PersonId = person.PersonId,
                    Fullname = person.Fullname,
                    BirthDay = person.BirthDay,
                    Phone = person.Phone,
                    viruses = viruses
                });
            }

            return new BaseResponseModel<List<getPersonReponseModel>>
            {
                Code = 200,
                Message = "OK",
                Data = personResponses
            };
        
        }
        public async Task<BaseResponseModel> deletePersonbyId(int id)
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(id);
            if (person == null)
            {
                return new BaseResponseModel()
                {
                    Code = 404,
                    Message = "Person not found",
                    
                };
            }
            // Lấy danh sách virus liên quan đến person
            var personViruses = await _unitOfWork.PersonVirusRepository.findVirusbyPersonId(id);

            // Xóa tất cả các bản ghi virus liên quan
            if (personViruses.Any())
            {
                foreach (var personVirus in personViruses)
                {
                    await _unitOfWork.PersonVirusRepository.RemoveAsync(personVirus);
                }
            }

            // Xóa person
            await _unitOfWork.PersonRepository.RemoveAsync(person);

            // Trả về kết quả thành công
            return new BaseResponseModel
            {
                Code = 200,
                Message = "Person and related viruses deleted successfully"
            };
        }

        public async Task<BaseResponseModel> UpdatePerson(int id ,UpdatePersonRequestmodel request)
        {
            if (request == null)
            {
                return new BaseResponseModel
                {
                    Code = 400,
                    Message = "Request is null"
                };
            }

            // Lấy thông tin person theo ID
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(id);

            // Kiểm tra nếu person không tồn tại
            if (person == null)
            {
                return new BaseResponseModel
                {
                    Code = 404,
                    Message = "Person not found"
                };
            }

            // Cập nhật thông tin person
            person.Fullname = request.Fullname;
            person.BirthDay = request.BirthDay;
            person.Phone = request.Phone;

            // Cập nhật danh sách virus
            if (request.viruses != null && request.viruses.Count > 0)
            {
                // Xóa các bản ghi virus cũ liên quan
                var existingViruses = await _unitOfWork.PersonVirusRepository.findVirusbyPersonId(person.PersonId);
                foreach (var existingVirus in existingViruses)
                {
                    await _unitOfWork.PersonVirusRepository.RemoveAsync(existingVirus);
                }

                // Thêm các virus mới
                foreach (var virusInfo in request.viruses)
                {
                    var virus = await _unitOfWork.VirusRepository.findVirusbyName(virusInfo.virusName);
                    if (virus != null)
                    {
                        PersonVirus personVirus = new PersonVirus
                        {
                            PersonId = person.PersonId,
                            VirusId = virus.VirusId,
                            ResistanceRate = virusInfo.resistanceRate
                        };

                        await _unitOfWork.PersonVirusRepository.CreateAsync(personVirus);
                    }
                }
            }

            // Cập nhật person trong cơ sở dữ liệu
            await _unitOfWork.PersonRepository.UpdateAsync(person);

            // Trả về kết quả thành công
            return new BaseResponseModel
            {
                Code = 200,
                Message = "Person and viruses updated successfully"
            };
        }
    }
}
