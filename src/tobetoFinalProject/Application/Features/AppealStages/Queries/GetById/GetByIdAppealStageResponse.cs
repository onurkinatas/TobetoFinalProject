using Core.Application.Responses;

namespace Application.Features.AppealStages.Queries.GetById;

public class GetByIdAppealStageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AppealId { get; set; }
    public Guid StageId { get; set; }
}