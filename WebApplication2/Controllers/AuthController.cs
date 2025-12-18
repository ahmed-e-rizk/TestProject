using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.BLL.Auth;
using Web.Core.Entites;
using Web.DTO.Auth;
using Web.DTO.Auth.Setting;

namespace Web.App.Controllers
{
    [Authorize]
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
        
        [HttpPost("login")]
        public IActionResult login(LoginDto loginDto)
        {
            var ff = _authBll.Login(loginDto);
            

            return Ok(ff);

        }
       
    }
}
