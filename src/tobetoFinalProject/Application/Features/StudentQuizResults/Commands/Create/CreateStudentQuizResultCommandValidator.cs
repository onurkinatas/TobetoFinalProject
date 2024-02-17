using FluentValidation;

namespace Application.Features.StudentQuizResults.Commands.Create;

public class CreateStudentQuizResultCommandValidator : AbstractValidator<CreateStudentQuizResultCommand>
{
    public CreateStudentQuizResultCommandValidator()
    {
        RuleFor(c => c.QuizId).NotEmpty();
    }
}