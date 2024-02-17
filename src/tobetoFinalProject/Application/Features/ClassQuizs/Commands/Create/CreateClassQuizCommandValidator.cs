using FluentValidation;

namespace Application.Features.ClassQuizs.Commands.Create;

public class CreateClassQuizCommandValidator : AbstractValidator<CreateClassQuizCommand>
{
    public CreateClassQuizCommandValidator()
    {
        RuleFor(c => c.StudentClassId).NotEmpty();
        RuleFor(c => c.QuizId).NotEmpty();
    }
}