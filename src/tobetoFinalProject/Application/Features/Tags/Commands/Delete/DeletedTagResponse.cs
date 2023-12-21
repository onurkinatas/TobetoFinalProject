using Core.Application.Responses;

namespace Application.Features.Tags.Commands.Delete;

public class DeletedTagResponse : IResponse
{
    public Guid Id { get; set; }
}