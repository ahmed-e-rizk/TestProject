using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.BLL.Auth;
using Web.DTO.Auth;

namespace Web.App.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : BaseController
    {

        private readonly IAuthBLL _authBll;
        public AuthController(IAuthBLL authBll)
        {
            _authBll = authBll;
        }


        [HttpPost("Register")]
        public IActionResult Register( RegisterDto registerDto)
        {

            var result = _authBll.Register(registerDto);

            return Ok(result);

        }
        

    }
}
