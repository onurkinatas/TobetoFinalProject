using Core.Application.Responses;

namespace Application.Features.ClassExams.Commands.Create;

public class CreatedClassExamResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentClassId { get; set; }
}