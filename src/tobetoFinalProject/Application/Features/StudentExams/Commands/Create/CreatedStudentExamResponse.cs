using Core.Application.Responses;

namespace Application.Features.StudentExams.Commands.Create;

public class CreatedStudentExamResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentId { get; set; }
}