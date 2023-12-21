using FluentValidation;

namespace Application.Features.StudentAppeals.Commands.Update;

public class UpdateStudentAppealCommandValidator : AbstractValidator<UpdateStudentAppealCommand>
{
    public UpdateStudentAppealCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.AppealId).NotEmpty();
    }
}