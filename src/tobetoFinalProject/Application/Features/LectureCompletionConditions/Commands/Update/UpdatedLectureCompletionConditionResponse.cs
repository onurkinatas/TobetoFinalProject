using Core.Application.Responses;

namespace Application.Features.LectureCompletionConditions.Commands.Update;

public class UpdatedLectureCompletionConditionResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public int CompletionPercentage { get; set; }
}