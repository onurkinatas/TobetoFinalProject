using Core.Application.Responses;

namespace Application.Features.ContentLikes.Commands.Update;

public class UpdatedContentLikeResponse : IResponse
{
    public Guid Id { get; set; }
    public bool IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid ContentId { get; set; }
}