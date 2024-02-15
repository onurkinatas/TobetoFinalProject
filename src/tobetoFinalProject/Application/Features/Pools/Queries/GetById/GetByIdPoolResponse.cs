using Core.Application.Responses;

namespace Application.Features.Pools.Queries.GetById;

public class GetByIdPoolResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}