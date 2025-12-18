using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Entites
{
    public class RefreshToken
    {

        public int? UserId { get; set; }

        public string? Jti { get; set; }

        public string? Token { get; set; }

        public DateTime? ExpireDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public virtual User? User { get; set; }
    }
}
