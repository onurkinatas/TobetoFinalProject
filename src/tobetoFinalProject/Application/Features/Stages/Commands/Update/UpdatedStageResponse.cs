using Core.Application.Responses;

namespace Application.Features.Stages.Commands.Update;

public class UpdatedStageResponse : IResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
}