using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.BLL.Response;
using Web.DTO.Auth;
using Web.DTO.Auth.Setting;

namespace Web.BLL.Auth
{
    public interface IAuthBLL
    {
        public bool Register (RegisterDto registerDto);
        public IResponse<TokenResultDto> Login (LoginDto registerDto);
    }
}
