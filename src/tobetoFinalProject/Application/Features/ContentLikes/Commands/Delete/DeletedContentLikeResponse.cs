using Core.Application.Responses;

namespace Application.Features.ContentLikes.Commands.Delete;

public class DeletedContentLikeResponse : IResponse
{
    public Guid Id { get; set; }
}