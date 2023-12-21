using Core.Application.Responses;

namespace Application.Features.AppealStages.Commands.Create;

public class CreatedAppealStageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AppealId { get; set; }
    public Guid StageId { get; set; }
}