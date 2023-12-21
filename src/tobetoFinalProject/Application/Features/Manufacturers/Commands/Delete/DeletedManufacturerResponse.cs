using Core.Application.Responses;

namespace Application.Features.Manufacturers.Commands.Delete;

public class DeletedManufacturerResponse : IResponse
{
    public Guid Id { get; set; }
}