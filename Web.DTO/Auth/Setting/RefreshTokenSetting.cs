using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTO.Auth.Setting
{
    public class RefreshTokenSetting
    {
        public int TokenLength { get; set; }

        public int RefreshTokenExpiryInMonths { get; set; }
    }
}
