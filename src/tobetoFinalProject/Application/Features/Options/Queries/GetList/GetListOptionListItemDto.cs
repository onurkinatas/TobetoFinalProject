using Core.Application.Dtos;

namespace Application.Features.Options.Queries.GetList;

public class GetListOptionListItemDto : IDto
{
    public int Id { get; set; }
    public string Text { get; set; }
}