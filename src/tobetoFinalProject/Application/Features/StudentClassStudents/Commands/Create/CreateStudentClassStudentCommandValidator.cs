using FluentValidation;

namespace Application.Features.StudentClassStudents.Commands.Create;

public class CreateStudentClassStudentCommandValidator : AbstractValidator<CreateStudentClassStudentCommand>
{
    public CreateStudentClassStudentCommandValidator()
    {
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.StudentClassId).NotEmpty();
    }
}