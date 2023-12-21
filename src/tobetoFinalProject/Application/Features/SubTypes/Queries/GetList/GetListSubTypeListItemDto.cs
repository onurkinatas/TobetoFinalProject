using Core.Application.Dtos;

namespace Application.Features.SubTypes.Queries.GetList;

public class GetListSubTypeListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}