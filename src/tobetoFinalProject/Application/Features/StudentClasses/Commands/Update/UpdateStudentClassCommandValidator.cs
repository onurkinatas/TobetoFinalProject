using FluentValidation;

namespace Application.Features.StudentClasses.Commands.Update;

public class UpdateStudentClassCommandValidator : AbstractValidator<UpdateStudentClassCommand>
{
    public UpdateStudentClassCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}