using FluentValidation;

namespace Application.Features.StudentClasses.Commands.Create;

public class CreateStudentClassCommandValidator : AbstractValidator<CreateStudentClassCommand>
{
    public CreateStudentClassCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}