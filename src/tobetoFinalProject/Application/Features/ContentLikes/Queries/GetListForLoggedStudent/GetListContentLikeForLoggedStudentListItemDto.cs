using Application.Features.Contents.Queries.GetById;
using Core.Application.Dtos;

namespace Application.Features.ContentLikes.Queries.GetListForLoggedStudent;

public class GetListContentLikeForLoggedStudentListItemDto : IDto
{
    public Guid Id { get; set; }
    public bool? IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid ContentId { get; set; }
    public GetByIdContentResponse? Content { get; set; }
}