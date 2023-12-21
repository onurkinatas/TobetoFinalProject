using Core.Application.Responses;

namespace Application.Features.LectureLikes.Commands.Update;

public class UpdatedLectureLikeResponse : IResponse
{
    public Guid Id { get; set; }
    public bool IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
}