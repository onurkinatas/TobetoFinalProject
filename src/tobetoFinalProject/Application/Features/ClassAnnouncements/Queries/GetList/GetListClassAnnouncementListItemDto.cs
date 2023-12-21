using Core.Application.Dtos;

namespace Application.Features.ClassAnnouncements.Queries.GetList;

public class GetListClassAnnouncementListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentClassId { get; set; }
}