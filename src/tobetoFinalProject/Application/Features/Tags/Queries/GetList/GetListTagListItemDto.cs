using Core.Application.Dtos;

namespace Application.Features.Tags.Queries.GetList;

public class GetListTagListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}