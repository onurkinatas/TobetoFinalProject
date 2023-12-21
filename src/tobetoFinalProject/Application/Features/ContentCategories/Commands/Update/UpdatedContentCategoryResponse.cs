using Core.Application.Responses;

namespace Application.Features.ContentCategories.Commands.Update;

public class UpdatedContentCategoryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}