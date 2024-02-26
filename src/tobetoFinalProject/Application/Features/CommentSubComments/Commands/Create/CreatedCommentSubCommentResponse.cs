using Core.Application.Responses;

namespace Application.Features.CommentSubComments.Commands.Create;

public class CreatedCommentSubCommentResponse : IResponse
{
    public int Id { get; set; }
    public int UserLectureCommentId { get; set; }
    public Guid StudentId { get; set; }
    public string SubComment { get; set; }
}