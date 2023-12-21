using Core.Application.Responses;

namespace Application.Features.ClassAnnouncements.Queries.GetById;

public class GetByIdClassAnnouncementResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentClassId { get; set; }
}