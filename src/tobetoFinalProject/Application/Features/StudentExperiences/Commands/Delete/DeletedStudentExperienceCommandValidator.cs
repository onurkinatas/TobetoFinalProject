using FluentValidation;

namespace Application.Features.StudentExperiences.Commands.Delete;

public class DeleteStudentExperienceCommandValidator : AbstractValidator<DeleteStudentExperienceCommand>
{
    public DeleteStudentExperienceCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}