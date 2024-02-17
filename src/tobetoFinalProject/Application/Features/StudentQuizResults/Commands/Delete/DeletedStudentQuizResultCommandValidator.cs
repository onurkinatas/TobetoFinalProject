using FluentValidation;

namespace Application.Features.StudentQuizResults.Commands.Delete;

public class DeleteStudentQuizResultCommandValidator : AbstractValidator<DeleteStudentQuizResultCommand>
{
    public DeleteStudentQuizResultCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}