using Core.Application.Dtos;

namespace Application.Features.LectureCourses.Queries.GetList;

public class GetListLectureCourseListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid LectureId { get; set; }
}