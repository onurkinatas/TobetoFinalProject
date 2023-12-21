using Core.Application.Responses;

namespace Application.Features.ContentCategories.Commands.Delete;

public class DeletedContentCategoryResponse : IResponse
{
    public Guid Id { get; set; }
}