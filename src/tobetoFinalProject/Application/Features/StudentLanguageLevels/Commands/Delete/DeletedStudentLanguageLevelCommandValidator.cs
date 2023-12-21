using FluentValidation;

namespace Application.Features.StudentLanguageLevels.Commands.Delete;

public class DeleteStudentLanguageLevelCommandValidator : AbstractValidator<DeleteStudentLanguageLevelCommand>
{
    public DeleteStudentLanguageLevelCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}