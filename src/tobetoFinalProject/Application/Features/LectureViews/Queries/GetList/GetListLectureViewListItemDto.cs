using Core.Application.Dtos;

namespace Application.Features.LectureViews.Queries.GetList;

public class GetListLectureViewListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public Guid ContentId { get; set; }
}