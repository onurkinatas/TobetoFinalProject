using Core.Application.Dtos;

namespace Application.Features.Manufacturers.Queries.GetList;

public class GetListManufacturerListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}