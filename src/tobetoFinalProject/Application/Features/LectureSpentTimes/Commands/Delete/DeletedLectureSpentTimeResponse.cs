using Core.Application.Responses;

namespace Application.Features.LectureSpentTimes.Commands.Delete;

public class DeletedLectureSpentTimeResponse : IResponse
{
    public Guid Id { get; set; }
}