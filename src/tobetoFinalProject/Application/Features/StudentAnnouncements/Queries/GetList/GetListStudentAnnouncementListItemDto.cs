using Core.Application.Dtos;

namespace Application.Features.StudentAnnouncements.Queries.GetList;

public class GetListStudentAnnouncementListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentId { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public string StudentEmail { get; set;}
    public string AnnouncementName { get; set; }
}