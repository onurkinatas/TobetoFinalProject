using Core.Application.Responses;

namespace Application.Features.SubTypes.Commands.Create;

public class CreatedSubTypeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}