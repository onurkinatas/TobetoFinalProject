using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.StudentLogin;
public class StudentLoginCommandValidator:AbstractValidator<StudentLoginCommand>
{
    public StudentLoginCommandValidator()
    {
        RuleFor(c => c.UserForLoginDto.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.UserForLoginDto.Password).NotEmpty().MinimumLength(4);
    }
}


