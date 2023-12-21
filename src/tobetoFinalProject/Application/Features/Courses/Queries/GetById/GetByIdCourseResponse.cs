using Application.Features.Contents.Queries.GetList;
using Core.Application.Responses;

namespace Application.Features.Courses.Queries.GetById;

public class GetByIdCourseResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<GetListContentListItemDto> Contents { get; set; }
}