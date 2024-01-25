using FluentValidation;

namespace Application.Features.StudentAnnouncements.Commands.Create;

public class CreateStudentAnnouncementCommandValidator : AbstractValidator<CreateStudentAnnouncementCommand>
{
    public CreateStudentAnnouncementCommandValidator()
    {
        RuleFor(c => c.AnnouncementId).NotEmpty();
    }
}