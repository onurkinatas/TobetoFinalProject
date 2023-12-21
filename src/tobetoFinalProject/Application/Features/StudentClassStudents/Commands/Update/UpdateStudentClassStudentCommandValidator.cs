using FluentValidation;

namespace Application.Features.StudentClassStudents.Commands.Update;

public class UpdateStudentClassStudentCommandValidator : AbstractValidator<UpdateStudentClassStudentCommand>
{
    public UpdateStudentClassStudentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.StudentClassId).NotEmpty();
    }
}