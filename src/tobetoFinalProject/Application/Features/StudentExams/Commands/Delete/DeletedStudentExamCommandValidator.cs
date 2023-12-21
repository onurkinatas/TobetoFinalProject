using FluentValidation;

namespace Application.Features.StudentExams.Commands.Delete;

public class DeleteStudentExamCommandValidator : AbstractValidator<DeleteStudentExamCommand>
{
    public DeleteStudentExamCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}