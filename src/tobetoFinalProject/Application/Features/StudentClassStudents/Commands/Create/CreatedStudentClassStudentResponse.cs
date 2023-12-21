using Core.Application.Responses;

namespace Application.Features.StudentClassStudents.Commands.Create;

public class CreatedStudentClassStudentResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid StudentClassId { get; set; }
}