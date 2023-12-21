using Core.Application.Dtos;

namespace Application.Features.ContentCategories.Queries.GetList;

public class GetListContentCategoryListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}