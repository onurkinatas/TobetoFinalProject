using Core.Application.Dtos;

namespace Application.Features.ClassLectures.Queries.GetList;

public class GetListClassLectureListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid LectureId { get; set; }
    public Guid StudentClassId { get; set; }
}