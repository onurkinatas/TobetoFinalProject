using Core.Application.Responses;

namespace Application.Features.StudentClassStudents.Commands.Update;

public class UpdatedStudentClassStudentResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid StudentClassId { get; set; }
}