using Core.Application.Responses;

namespace Application.Features.Tags.Commands.Update;

public class UpdatedTagResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}