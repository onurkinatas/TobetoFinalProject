using FluentValidation;

namespace Application.Features.StudentEducations.Commands.Update;

public class UpdateStudentEducationCommandValidator : AbstractValidator<UpdateStudentEducationCommand>
{
    public UpdateStudentEducationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.EducationStatus).NotEmpty();
        RuleFor(c => c.SchoolName).NotEmpty();
        RuleFor(c => c.Branch).NotEmpty();
        RuleFor(c => c.IsContinued).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.GraduationDate).NotEmpty();
    }
}