using Core.Application.Responses;

namespace Application.Features.Stages.Commands.Create;

public class CreatedStageResponse : IResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
}