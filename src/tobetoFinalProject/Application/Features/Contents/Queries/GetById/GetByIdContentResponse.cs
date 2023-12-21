using Application.Features.Instructors.Queries.GetList;
using Core.Application.Responses;

namespace Application.Features.Contents.Queries.GetById;

public class GetByIdContentResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LanguageName { get; set; }
    public string Description { get; set; }
    public string SubTypeName { get; set; }
    public string VideoUrl { get; set; }
    public int Duration { get; set; }
    public string? ContentCategoryName { get; set; }
    public string ManufacturerName { get; set; }
    public ICollection<GetListInstructorListItemDto>? Instructors { get; set; }
}