using FluentValidation;

namespace Application.Features.ClassAnnouncements.Commands.Delete;

public class DeleteClassAnnouncementCommandValidator : AbstractValidator<DeleteClassAnnouncementCommand>
{
    public DeleteClassAnnouncementCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}