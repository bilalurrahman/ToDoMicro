using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace Authentication.Application.Features.Login
{
    public class LoginViolator:AbstractValidator<LoginRequest>
    {
        public LoginViolator()
        {
            RuleFor(u => u.username)
             .NotEmpty().WithMessage("{username} is Required")
             .NotNull().WithMessage("{username} is Required");



            RuleFor(u => u.password)
             .NotEmpty().WithMessage("{password} is Required")
             .NotNull().WithMessage("{password} is Required");
             
        }
    }
}
