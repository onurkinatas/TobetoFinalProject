using Core.Application.Responses;

namespace Application.Features.Exams.Commands.Create;

public class CreatedExamResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public string ExamUrl { get; set; }
}