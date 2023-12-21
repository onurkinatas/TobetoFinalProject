using FluentValidation;

namespace Application.Features.StudentEducations.Commands.Create;

public class CreateStudentEducationCommandValidator : AbstractValidator<CreateStudentEducationCommand>
{
    public CreateStudentEducationCommandValidator()
    {
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.EducationStatus).NotEmpty();
        RuleFor(c => c.SchoolName).NotEmpty();
        RuleFor(c => c.Branch).NotEmpty();
        RuleFor(c => c.IsContinued).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.GraduationDate).NotEmpty();
    }
}