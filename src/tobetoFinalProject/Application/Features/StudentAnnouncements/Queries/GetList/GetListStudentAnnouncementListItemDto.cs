using Core.Application.Dtos;

namespace Application.Features.StudentAnnouncements.Queries.GetList;

public class GetListStudentAnnouncementListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentId { get; set; }
}