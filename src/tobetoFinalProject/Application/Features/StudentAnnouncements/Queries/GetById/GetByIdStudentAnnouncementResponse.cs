using Core.Application.Responses;

namespace Application.Features.StudentAnnouncements.Queries.GetById;

public class GetByIdStudentAnnouncementResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentId { get; set; }
}