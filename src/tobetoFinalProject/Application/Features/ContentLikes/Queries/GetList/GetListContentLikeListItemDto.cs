using Core.Application.Dtos;

namespace Application.Features.ContentLikes.Queries.GetList;

public class GetListContentLikeListItemDto : IDto
{
    public Guid Id { get; set; }
    public bool IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid ContentId { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public string StudentEmail { get; set; }
    public string ContentName { get; set; }
}