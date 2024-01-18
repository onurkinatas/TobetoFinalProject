using Core.Application.Dtos;

namespace Application.Features.StudentAnnouncements.Queries.GetList;

public class GetListStudentAnnouncementListItemDto : IDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentId { get; set; }
}