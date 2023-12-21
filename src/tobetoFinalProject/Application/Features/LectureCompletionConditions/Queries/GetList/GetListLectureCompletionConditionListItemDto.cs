using Core.Application.Dtos;

namespace Application.Features.LectureCompletionConditions.Queries.GetList;

public class GetListLectureCompletionConditionListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public int CompletionPercentage { get; set; }
}