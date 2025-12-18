using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTO.Auth
{
    public class PhoneDto
    {
        public string Phone { get; set; }
        public bool Iswhatsapp { get; set; } = false;
    }
}
