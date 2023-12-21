using FluentValidation;

namespace Application.Features.ClassAnnouncements.Commands.Update;

public class UpdateClassAnnouncementCommandValidator : AbstractValidator<UpdateClassAnnouncementCommand>
{
    public UpdateClassAnnouncementCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.AnnouncementId).NotEmpty();
        RuleFor(c => c.StudentClassId).NotEmpty();
    }
}