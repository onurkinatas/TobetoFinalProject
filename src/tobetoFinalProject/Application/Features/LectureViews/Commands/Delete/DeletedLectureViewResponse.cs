using Core.Application.Responses;

namespace Application.Features.LectureViews.Commands.Delete;

public class DeletedLectureViewResponse : IResponse
{
    public Guid Id { get; set; }
}