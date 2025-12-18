using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTO.Auth
{
    public class LoginDto
    {
        [EmailAddress]
        [NotNull]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
