using Core.Application.Responses;

namespace Application.Features.LectureCourses.Commands.Create;

public class CreatedLectureCourseResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid LectureId { get; set; }
}