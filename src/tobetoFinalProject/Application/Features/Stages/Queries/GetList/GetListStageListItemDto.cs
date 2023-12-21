using Core.Application.Dtos;

namespace Application.Features.Stages.Queries.GetList;

public class GetListStageListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
}