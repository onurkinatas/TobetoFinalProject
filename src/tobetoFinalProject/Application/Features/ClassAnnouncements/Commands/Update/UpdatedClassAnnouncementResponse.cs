using Core.Application.Responses;

namespace Application.Features.ClassAnnouncements.Commands.Update;

public class UpdatedClassAnnouncementResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentClassId { get; set; }
}