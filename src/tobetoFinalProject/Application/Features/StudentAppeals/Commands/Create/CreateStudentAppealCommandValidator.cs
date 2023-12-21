using FluentValidation;

namespace Application.Features.StudentAppeals.Commands.Create;

public class CreateStudentAppealCommandValidator : AbstractValidator<CreateStudentAppealCommand>
{
    public CreateStudentAppealCommandValidator()
    {
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.AppealId).NotEmpty();
    }
}