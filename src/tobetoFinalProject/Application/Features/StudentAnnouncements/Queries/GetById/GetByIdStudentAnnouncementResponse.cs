using Core.Application.Responses;

namespace Application.Features.StudentAnnouncements.Queries.GetById;

public class GetByIdStudentAnnouncementResponse : IResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentId { get; set; }
}