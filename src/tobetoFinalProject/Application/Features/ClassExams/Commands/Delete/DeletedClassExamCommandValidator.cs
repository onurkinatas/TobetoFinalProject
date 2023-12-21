using FluentValidation;

namespace Application.Features.ClassExams.Commands.Delete;

public class DeleteClassExamCommandValidator : AbstractValidator<DeleteClassExamCommand>
{
    public DeleteClassExamCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}