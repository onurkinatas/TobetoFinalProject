using Core.Application.Dtos;

namespace Application.Features.Pools.Queries.GetList;

public class GetListPoolListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}