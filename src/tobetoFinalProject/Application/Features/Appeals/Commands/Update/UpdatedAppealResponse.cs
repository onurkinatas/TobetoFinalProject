using Core.Application.Responses;

namespace Application.Features.Appeals.Commands.Update;

public class UpdatedAppealResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}