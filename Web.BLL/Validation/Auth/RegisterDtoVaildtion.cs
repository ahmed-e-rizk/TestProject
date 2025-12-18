using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.BLL.Validation.Auth
{
    public class RegisterDtoVaildtion :AbstractValidator<Web.DTO.Auth.RegisterDto>
    {
        public RegisterDtoVaildtion()
        {
           RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                 .MinimumLength(3).MaximumLength(50).WithMessage("Name must be between 3 and 50 characters long");
    
              RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                 .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password).NotEmpty().WithErrorCode("Password is required")
                 .MinimumLength(6).MaximumLength(10).WithMessage("Password must be between 6 and 10 characters long");



        }
    }
}
