using Core.Application.Responses;

namespace Application.Features.Manufacturers.Queries.GetById;

public class GetByIdManufacturerResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}