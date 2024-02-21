using Application.Features.Instructors.Queries.GetList;
using Application.Features.Tags.Queries.GetList;
using Core.Application.Dtos;

namespace Application.Features.Contents.Queries.GetList;

public class GetListContentListItemDto : IDto
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
    public ICollection<GetListTagListItemDto>? Tags { get; set; }
}