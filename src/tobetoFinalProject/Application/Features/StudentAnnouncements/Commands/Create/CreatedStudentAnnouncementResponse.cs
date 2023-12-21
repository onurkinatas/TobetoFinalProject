using Core.Application.Responses;

namespace Application.Features.StudentAnnouncements.Commands.Create;

public class CreatedStudentAnnouncementResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentId { get; set; }
}