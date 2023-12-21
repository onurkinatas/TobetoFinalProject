using FluentValidation;

namespace Application.Features.StudentAnnouncements.Commands.Delete;

public class DeleteStudentAnnouncementCommandValidator : AbstractValidator<DeleteStudentAnnouncementCommand>
{
    public DeleteStudentAnnouncementCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}