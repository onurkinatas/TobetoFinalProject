using Core.Application.Responses;

namespace Application.Features.LectureCourses.Commands.Update;

public class UpdatedLectureCourseResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid LectureId { get; set; }
}