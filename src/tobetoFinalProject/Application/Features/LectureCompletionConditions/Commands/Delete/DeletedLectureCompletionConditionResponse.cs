using Core.Application.Responses;

namespace Application.Features.LectureCompletionConditions.Commands.Delete;

public class DeletedLectureCompletionConditionResponse : IResponse
{
    public Guid Id { get; set; }
}