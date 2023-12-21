using Core.Application.Responses;

namespace Application.Features.ContentCategories.Commands.Create;

public class CreatedContentCategoryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}