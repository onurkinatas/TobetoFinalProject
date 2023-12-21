using Core.Application.Responses;

namespace Application.Features.Appeals.Commands.Create;

public class CreatedAppealResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}