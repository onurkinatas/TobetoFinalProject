using Core.Application.Responses;

namespace Application.Features.SubTypes.Commands.Delete;

public class DeletedSubTypeResponse : IResponse
{
    public Guid Id { get; set; }
}