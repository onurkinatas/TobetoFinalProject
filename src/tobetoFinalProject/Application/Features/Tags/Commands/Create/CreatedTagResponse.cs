using Core.Application.Responses;

namespace Application.Features.Tags.Commands.Create;

public class CreatedTagResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}