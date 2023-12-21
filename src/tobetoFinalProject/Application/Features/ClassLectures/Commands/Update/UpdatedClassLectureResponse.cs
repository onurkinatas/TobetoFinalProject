using Core.Application.Responses;

namespace Application.Features.ClassLectures.Commands.Update;

public class UpdatedClassLectureResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid LectureId { get; set; }
    public Guid StudentClassId { get; set; }
}