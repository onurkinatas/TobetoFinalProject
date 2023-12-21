using Core.Application.Responses;

namespace Application.Features.CourseContents.Commands.Delete;

public class DeletedCourseContentResponse : IResponse
{
    public Guid Id { get; set; }
}