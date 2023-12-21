using FluentValidation;

namespace Application.Features.StudentExperiences.Commands.Update;

public class UpdateStudentExperienceCommandValidator : AbstractValidator<UpdateStudentExperienceCommand>
{
    public UpdateStudentExperienceCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.CompanyName).NotEmpty();
        RuleFor(c => c.Sector).NotEmpty();
        RuleFor(c => c.Position).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.CityId).NotEmpty();
    }
}