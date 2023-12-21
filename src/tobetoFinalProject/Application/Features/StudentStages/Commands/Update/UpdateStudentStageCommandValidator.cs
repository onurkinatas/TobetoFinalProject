using FluentValidation;

namespace Application.Features.StudentStages.Commands.Update;

public class UpdateStudentStageCommandValidator : AbstractValidator<UpdateStudentStageCommand>
{
    public UpdateStudentStageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StageId).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
    }
}