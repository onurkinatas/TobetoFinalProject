using Core.Application.Dtos;

namespace Application.Features.ClassAnnouncements.Queries.GetListForLoggedStudent;

public class GetListForLoggedStudentClassAnnouncementListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public string AnnouncementName { get; set; }
    public string AnnouncementDescription { get; set; }
    public string StudentClassName { get; set; }
    public bool? IsRead { get; set; }
    public DateTime AnnouncementCreatedDate { get; set; }
}