using BusinessLayer.CategoryBusiness;
using BusinessLayer.RequestModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ViroCure_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> createPerson([FromBody] PersonRequestModel request)
        {
            var response = await _personService.createPerson(request);
            return StatusCode((int)response.Code, response);
        }
        [HttpGet("GetPersonbyId")]
        public async Task<IActionResult> createPerson(int id)
        {
            var response = await _personService.getPersonById(id);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("getAllPerson")]
        public async Task<IActionResult> getAllPerson()
        {
            var response = await _personService.getAllPerson();
            return StatusCode((int)response.Code, response);
        }
        [HttpDelete("DeletePerson")]
        public async Task<IActionResult> DeletePersonbyId(int id)
        {
            var response = await _personService.deletePersonbyId(id);
            return StatusCode((int)response.Code, response);
        }
        [HttpDelete("UpdatePerson/{id}")]
        public async Task<IActionResult> DeletePersonbyId(int id, [FromBody] UpdatePersonRequestmodel request)
        {
            var response = await _personService.UpdatePerson(id,request);
            return StatusCode((int)response.Code, response);
        }
    }
}
