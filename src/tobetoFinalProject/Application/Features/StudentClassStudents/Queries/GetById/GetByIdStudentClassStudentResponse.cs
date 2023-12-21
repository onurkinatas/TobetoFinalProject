using Core.Application.Responses;

namespace Application.Features.StudentClassStudents.Queries.GetById;

public class GetByIdStudentClassStudentResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid StudentClassId { get; set; }
}