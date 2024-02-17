using FluentValidation;

namespace Application.Features.ClassQuizs.Commands.Update;

public class UpdateClassQuizCommandValidator : AbstractValidator<UpdateClassQuizCommand>
{
    public UpdateClassQuizCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StudentClassId).NotEmpty();
        RuleFor(c => c.QuizId).NotEmpty();
    }
}