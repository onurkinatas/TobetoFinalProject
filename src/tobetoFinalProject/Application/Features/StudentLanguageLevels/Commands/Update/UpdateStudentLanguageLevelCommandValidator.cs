using FluentValidation;

namespace Application.Features.StudentLanguageLevels.Commands.Update;

public class UpdateStudentLanguageLevelCommandValidator : AbstractValidator<UpdateStudentLanguageLevelCommand>
{
    public UpdateStudentLanguageLevelCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.LanguageLevelId).NotEmpty();
    }
}