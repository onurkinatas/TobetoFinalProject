using FluentValidation;

namespace Application.Features.StudentStages.Commands.Delete;

public class DeleteStudentStageCommandValidator : AbstractValidator<DeleteStudentStageCommand>
{
    public DeleteStudentStageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}