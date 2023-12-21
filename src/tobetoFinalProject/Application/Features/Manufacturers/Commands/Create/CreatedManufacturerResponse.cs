using Core.Application.Responses;

namespace Application.Features.Manufacturers.Commands.Create;

public class CreatedManufacturerResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}