using Core.Application.Dtos;

namespace Application.Features.Appeals.Queries.GetList;

public class GetListAppealListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}