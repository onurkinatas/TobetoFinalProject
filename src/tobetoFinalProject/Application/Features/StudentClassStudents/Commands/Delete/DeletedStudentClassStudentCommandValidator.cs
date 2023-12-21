using FluentValidation;

namespace Application.Features.StudentClassStudents.Commands.Delete;

public class DeleteStudentClassStudentCommandValidator : AbstractValidator<DeleteStudentClassStudentCommand>
{
    public DeleteStudentClassStudentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}