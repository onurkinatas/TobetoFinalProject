using Core.Application.Responses;

namespace Application.Features.Options.Commands.Create;

public class CreatedOptionResponse : IResponse
{
    public int Id { get; set; }
    public string Text { get; set; }
}