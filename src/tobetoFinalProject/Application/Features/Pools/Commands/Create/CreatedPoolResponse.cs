using Core.Application.Responses;

namespace Application.Features.Pools.Commands.Create;

public class CreatedPoolResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}