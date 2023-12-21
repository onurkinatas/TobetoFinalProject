using FluentValidation;

namespace Application.Features.ContentInstructors.Commands.Update;

public class UpdateContentInstructorCommandValidator : AbstractValidator<UpdateContentInstructorCommand>
{
    public UpdateContentInstructorCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.InstructorId).NotEmpty();
    }
}