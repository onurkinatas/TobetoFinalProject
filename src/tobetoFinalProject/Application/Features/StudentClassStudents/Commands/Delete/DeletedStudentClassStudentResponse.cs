using Core.Application.Responses;

namespace Application.Features.StudentClassStudents.Commands.Delete;

public class DeletedStudentClassStudentResponse : IResponse
{
    public Guid Id { get; set; }
}