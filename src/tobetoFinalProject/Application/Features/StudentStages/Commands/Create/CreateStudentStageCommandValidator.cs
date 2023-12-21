using FluentValidation;

namespace Application.Features.StudentStages.Commands.Create;

public class CreateStudentStageCommandValidator : AbstractValidator<CreateStudentStageCommand>
{
    public CreateStudentStageCommandValidator()
    {
        RuleFor(c => c.StageId).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
    }
}