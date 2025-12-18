using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.BLL.Validation.Auth
{
    public class LoginDtoVailidation :AbstractValidator<Web.DTO.Auth.LoginDto>
    {
        public LoginDtoVailidation()
        {
           RuleFor(x => x.UserName).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithErrorCode("DDD").WithMessage("Invalid email format");

                //        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                //.MinimumLength(6).MaximumLength(10).WithMessage("Password must be at least 6 characters long");



            RuleFor(x => x.Password)
    .NotEmpty().WithMessage("Password is required")
    .When(x => x.UserName != string.Empty);
        }
    }
}
