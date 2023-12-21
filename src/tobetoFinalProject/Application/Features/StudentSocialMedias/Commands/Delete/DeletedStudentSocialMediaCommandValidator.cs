using FluentValidation;

namespace Application.Features.StudentSocialMedias.Commands.Delete;

public class DeleteStudentSocialMediaCommandValidator : AbstractValidator<DeleteStudentSocialMediaCommand>
{
    public DeleteStudentSocialMediaCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}