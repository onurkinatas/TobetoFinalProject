using Core.Application.Responses;

namespace Application.Features.LectureCourses.Queries.GetById;

public class GetByIdLectureCourseResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid LectureId { get; set; }
}