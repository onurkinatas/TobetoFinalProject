using FluentValidation;

namespace Application.Features.QuizQuestions.Commands.Update;

public class UpdateQuizQuestionCommandValidator : AbstractValidator<UpdateQuizQuestionCommand>
{
    public UpdateQuizQuestionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.QuizId).NotEmpty();
        RuleFor(c => c.QuestionId).NotEmpty();
    }
}