using Core.Application.Responses;

namespace Application.Features.StudentAnnouncements.Commands.Update;

public class UpdatedStudentAnnouncementResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentId { get; set; }
}