using Core.Application.Dtos;

namespace Application.Features.LectureLikes.Queries.GetList;

public class GetListLectureLikeListItemDto : IDto
{
    public Guid Id { get; set; }
    public bool IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
}