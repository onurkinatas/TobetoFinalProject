using Core.Application.Responses;

namespace Application.Features.StudentExams.Commands.Update;

public class UpdatedStudentExamResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentId { get; set; }
}