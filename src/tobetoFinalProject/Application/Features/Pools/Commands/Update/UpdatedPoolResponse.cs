using Core.Application.Responses;

namespace Application.Features.Pools.Commands.Update;

public class UpdatedPoolResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}