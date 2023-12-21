using Core.Application.Dtos;

namespace Application.Features.LectureSpentTimes.Queries.GetList;

public class GetListLectureSpentTimeListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public double SpentedTime { get; set; }
}