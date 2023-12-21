using FluentValidation;

namespace Application.Features.StudentAppeals.Commands.Delete;

public class DeleteStudentAppealCommandValidator : AbstractValidator<DeleteStudentAppealCommand>
{
    public DeleteStudentAppealCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}