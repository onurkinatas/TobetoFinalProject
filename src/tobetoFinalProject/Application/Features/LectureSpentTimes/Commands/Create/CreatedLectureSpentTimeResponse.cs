using Core.Application.Responses;

namespace Application.Features.LectureSpentTimes.Commands.Create;

public class CreatedLectureSpentTimeResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public double SpentedTime { get; set; }
}