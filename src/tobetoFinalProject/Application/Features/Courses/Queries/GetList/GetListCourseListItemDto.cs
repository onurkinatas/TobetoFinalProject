using Application.Features.Contents.Queries.GetList;
using Core.Application.Dtos;

namespace Application.Features.Courses.Queries.GetList;

public class GetListCourseListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<GetListContentListItemDto> Contents { get; set; }
}