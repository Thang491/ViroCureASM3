using BusinessLayer.CategoryBusiness;
using BusinessLayer.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ViroCure_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ViroCureController : ControllerBase
    {
        private readonly IViroCureUserService _userService;
        public ViroCureController(IViroCureUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var response = await _userService.Login(request.Email,request.Password);
            return StatusCode((int)response.Code, response);
        }
    }
}
