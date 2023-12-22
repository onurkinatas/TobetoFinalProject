using Core.Application.Responses;

namespace Application.Features.ClassExams.Queries.GetById;

public class GetByIdClassExamResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentClassId { get; set; }
    public string ExamName { get; set; }
    public bool ExamIsActive { get; set; }
    public string ExamExamUrl { get; set; }
    public string StudentClassName { get; set; }
}