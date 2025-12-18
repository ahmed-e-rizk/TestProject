using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTO.Auth.Setting
{
    public partial class RefreshTokenDto
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string? Jti { get; set; }

        public string? Token { get; set; }

        public DateTime? ExpireDate { get; set; }

    }
}
