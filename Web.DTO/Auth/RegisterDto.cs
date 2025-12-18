using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTO.Auth
{
    public class RegisterDto
    {
        public string? Name { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public DateOnly? DateOfbirth { get; set; }

        public bool? IsParent { get; set; }


        public  List<AddressDto> Addresses { get; set; } = new List<AddressDto>();

        public  List<PhoneDto> Phones { get; set; } = new List<PhoneDto>();
    }

  
    

}
