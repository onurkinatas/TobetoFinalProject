using Core.Application.Dtos;

namespace Application.Features.ContentTags.Queries.GetList;

public class GetListContentTagListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ContentId { get; set; }
    public Guid TagId { get; set; }
}