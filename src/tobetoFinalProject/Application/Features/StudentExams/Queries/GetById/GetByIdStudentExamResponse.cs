using Core.Application.Responses;

namespace Application.Features.StudentExams.Queries.GetById;

public class GetByIdStudentExamResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid? StudentId { get; set; }
}