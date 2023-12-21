using FluentValidation;

namespace Application.Features.StudentLanguageLevels.Commands.Create;

public class CreateStudentLanguageLevelCommandValidator : AbstractValidator<CreateStudentLanguageLevelCommand>
{
    public CreateStudentLanguageLevelCommandValidator()
    {
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.LanguageLevelId).NotEmpty();
    }
}