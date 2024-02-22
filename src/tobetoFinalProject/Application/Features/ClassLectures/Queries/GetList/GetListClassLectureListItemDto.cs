using Core.Application.Dtos;

namespace Application.Features.ClassLectures.Queries.GetList;

public class GetListClassLectureListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid LectureId { get; set; }
    public string LectureName { get; set; }
    public string LectureImageUrl { get; set; }
    public DateTime LectureStartDate { get; set; }
    public DateTime LectureEndDate { get; set; }
}