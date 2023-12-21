using Core.Application.Responses;

namespace Application.Features.ClassAnnouncements.Commands.Create;

public class CreatedClassAnnouncementResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentClassId { get; set; }
}