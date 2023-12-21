using Core.Application.Dtos;

namespace Application.Features.ContentLikes.Queries.GetList;

public class GetListContentLikeListItemDto : IDto
{
    public Guid Id { get; set; }
    public bool IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid ContentId { get; set; }
}