using Core.Application.Dtos;

namespace Application.Features.CourseContents.Queries.GetList;

public class GetListCourseContentListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid ContentId { get; set; }
}