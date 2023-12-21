using Core.Application.Responses;

namespace Application.Features.Manufacturers.Commands.Update;

public class UpdatedManufacturerResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}