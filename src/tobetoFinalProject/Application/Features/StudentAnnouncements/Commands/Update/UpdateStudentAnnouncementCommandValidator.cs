using FluentValidation;

namespace Application.Features.StudentAnnouncements.Commands.Update;

public class UpdateStudentAnnouncementCommandValidator : AbstractValidator<UpdateStudentAnnouncementCommand>
{
    public UpdateStudentAnnouncementCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.AnnouncementId).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
    }
}