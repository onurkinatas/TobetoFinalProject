using FluentValidation;

namespace Application.Features.ClassAnnouncements.Commands.Create;

public class CreateClassAnnouncementCommandValidator : AbstractValidator<CreateClassAnnouncementCommand>
{
    public CreateClassAnnouncementCommandValidator()
    {
        RuleFor(c => c.AnnouncementId).NotEmpty();
        RuleFor(c => c.StudentClassId).NotEmpty();
    }
}