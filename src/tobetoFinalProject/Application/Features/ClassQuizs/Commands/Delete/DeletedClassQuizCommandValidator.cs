using FluentValidation;

namespace Application.Features.ClassQuizs.Commands.Delete;

public class DeleteClassQuizCommandValidator : AbstractValidator<DeleteClassQuizCommand>
{
    public DeleteClassQuizCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}