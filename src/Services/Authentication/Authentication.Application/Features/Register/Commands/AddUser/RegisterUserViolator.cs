using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.Register.Commands.AddUser
{
   public class RegisterUserViolator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserViolator()
        {
            RuleFor(u => u.Username)
             .NotEmpty().WithMessage("{Username} is Required")
             .NotNull().WithMessage("{Username} is Required")
             .MinimumLength(6).WithMessage("{Username}  Minimum limit is 6")
            .MaximumLength(50).WithMessage("{Username} Max limit is 50");


            RuleFor(u => u.Password)
                 .NotEmpty().WithMessage("{Password} is Required")
                 .NotNull().WithMessage("{Password} is Required")
                 .MinimumLength(6).WithMessage("{Password} Minimum limit is 6")
                 .MaximumLength(16).WithMessage("{Password} Max limit is 16"); ;
        }
        
    }
}
