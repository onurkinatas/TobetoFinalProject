using Core.Application.Responses;

namespace Application.Features.ClassExams.Queries.GetById;

public class GetByIdClassExamResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentClassId { get; set; }
}