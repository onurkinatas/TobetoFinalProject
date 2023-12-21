using Core.Application.Dtos;

namespace Application.Features.AppealStages.Queries.GetList;

public class GetListAppealStageListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid AppealId { get; set; }
    public Guid StageId { get; set; }
}