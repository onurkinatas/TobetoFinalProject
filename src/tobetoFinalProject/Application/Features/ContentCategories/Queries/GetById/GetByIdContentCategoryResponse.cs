using Core.Application.Responses;

namespace Application.Features.ContentCategories.Queries.GetById;

public class GetByIdContentCategoryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}