using Core.Application.Dtos;

namespace Application.Features.ContentInstructors.Queries.GetList;

public class GetListContentInstructorListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ContentId { get; set; }
    public Guid InstructorId { get; set; }
}