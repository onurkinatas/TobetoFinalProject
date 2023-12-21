using Core.Application.Responses;

namespace Application.Features.LectureCourses.Commands.Delete;

public class DeletedLectureCourseResponse : IResponse
{
    public Guid Id { get; set; }
}