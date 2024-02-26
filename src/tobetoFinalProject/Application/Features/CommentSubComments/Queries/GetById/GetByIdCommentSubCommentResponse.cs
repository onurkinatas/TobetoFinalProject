using Core.Application.Responses;

namespace Application.Features.CommentSubComments.Queries.GetById;

public class GetByIdCommentSubCommentResponse : IResponse
{
    public int Id { get; set; }
    public int UserLectureCommentId { get; set; }
    public Guid StudentId { get; set; }
    public string SubComment { get; set; }
}