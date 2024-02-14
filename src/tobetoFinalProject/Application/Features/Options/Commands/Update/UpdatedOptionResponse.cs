using Core.Application.Responses;

namespace Application.Features.Options.Commands.Update;

public class UpdatedOptionResponse : IResponse
{
    public int Id { get; set; }
    public string Text { get; set; }
}