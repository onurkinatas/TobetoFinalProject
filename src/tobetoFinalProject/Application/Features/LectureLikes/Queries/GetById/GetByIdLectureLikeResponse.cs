using Core.Application.Responses;

namespace Application.Features.LectureLikes.Queries.GetById;

public class GetByIdLectureLikeResponse : IResponse
{
    public Guid Id { get; set; }
    public bool IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
}