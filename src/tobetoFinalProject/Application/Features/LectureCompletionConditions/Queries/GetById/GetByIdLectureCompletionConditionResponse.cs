using Core.Application.Responses;

namespace Application.Features.LectureCompletionConditions.Queries.GetById;

public class GetByIdLectureCompletionConditionResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public int CompletionPercentage { get; set; }
}