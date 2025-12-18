using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTO.Auth.Setting
{
    public class Jwt
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public TimeSpan TokenExpiryTime { get; set; }

        public RefreshTokenSetting RefreshToken { get; set; } = new();

    }
}
