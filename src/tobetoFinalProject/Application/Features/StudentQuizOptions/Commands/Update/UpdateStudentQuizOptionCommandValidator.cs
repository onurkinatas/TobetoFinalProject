using FluentValidation;

namespace Application.Features.StudentQuizOptions.Commands.Update;

public class UpdateStudentQuizOptionCommandValidator : AbstractValidator<UpdateStudentQuizOptionCommand>
{
    public UpdateStudentQuizOptionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ExamId).NotEmpty();
        RuleFor(c => c.QuizId).NotEmpty();
        RuleFor(c => c.QuestionId).NotEmpty();
        RuleFor(c => c.OptionId).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
    }
}