using Core.Application.Responses;

namespace Application.Features.CommentSubComments.Commands.Delete;

public class DeletedCommentSubCommentResponse : IResponse
{
    public int Id { get; set; }
}