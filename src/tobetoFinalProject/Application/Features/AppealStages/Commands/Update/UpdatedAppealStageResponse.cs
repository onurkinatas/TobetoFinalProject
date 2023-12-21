using Core.Application.Responses;

namespace Application.Features.AppealStages.Commands.Update;

public class UpdatedAppealStageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AppealId { get; set; }
    public Guid StageId { get; set; }
}