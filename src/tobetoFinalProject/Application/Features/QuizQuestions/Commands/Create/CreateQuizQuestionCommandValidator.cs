using FluentValidation;

namespace Application.Features.QuizQuestions.Commands.Create;

public class CreateQuizQuestionCommandValidator : AbstractValidator<CreateQuizQuestionCommand>
{
    public CreateQuizQuestionCommandValidator()
    {
        RuleFor(c => c.QuizId).NotEmpty();
        RuleFor(c => c.QuestionId).NotEmpty();
    }
}