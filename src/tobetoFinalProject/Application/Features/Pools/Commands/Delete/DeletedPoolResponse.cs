using Core.Application.Responses;

namespace Application.Features.Pools.Commands.Delete;

public class DeletedPoolResponse : IResponse
{
    public int Id { get; set; }
}