using FluentValidation;

namespace Application.Features.StudentQuizOptions.Commands.Create;

public class CreateStudentQuizOptionCommandValidator : AbstractValidator<CreateStudentQuizOptionCommand>
{
    public CreateStudentQuizOptionCommandValidator()
    {
        RuleFor(c => c.QuizId).NotEmpty();
        RuleFor(c => c.QuestionId).NotEmpty();
    }
}