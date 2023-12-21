using Core.Application.Responses;

namespace Application.Features.ContentLikes.Commands.Create;

public class CreatedContentLikeResponse : IResponse
{
    public Guid Id { get; set; }
    public bool IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid ContentId { get; set; }
}