using Core.Application.Responses;

namespace Application.Features.StudentExams.Commands.Delete;

public class DeletedStudentExamResponse : IResponse
{
    public Guid Id { get; set; }
}