using FluentValidation;

namespace Application.Features.StudentQuizResults.Commands.Update;

public class UpdateStudentQuizResultCommandValidator : AbstractValidator<UpdateStudentQuizResultCommand>
{
    public UpdateStudentQuizResultCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.QuizId).NotEmpty();
        RuleFor(c => c.CorrectAnswerCount).NotEmpty();
        RuleFor(c => c.WrongAnswerCount).NotEmpty();
        RuleFor(c => c.EmptyAnswerCount).NotEmpty();
        RuleFor(c => c.Student).NotEmpty();
        RuleFor(c => c.Quiz).NotEmpty();
    }
}