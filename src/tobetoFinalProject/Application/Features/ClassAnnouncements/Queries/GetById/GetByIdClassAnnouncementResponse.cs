using Core.Application.Responses;

namespace Application.Features.ClassAnnouncements.Queries.GetById;

public class GetByIdClassAnnouncementResponse : IResponse
{
    public Guid Id { get; set; }
    public string AnnouncementName { get; set; }
    public string AnnouncementDescription { get; set; }
    public string StudentClassName { get; set; }
    public DateTime AnnouncementCreatedDate { get; set; }
}