using Core.Application.Responses;

namespace Application.Features.LectureViews.Commands.Update;

public class UpdatedLectureViewResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public Guid ContentId { get; set; }
}