using Core.Application.Dtos;

namespace Application.Features.StudentClassStudents.Queries.GetList;

public class GetListStudentClassStudentListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid StudentClassId { get; set; }
}