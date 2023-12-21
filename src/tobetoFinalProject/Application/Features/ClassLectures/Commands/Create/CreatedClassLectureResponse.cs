using Core.Application.Responses;

namespace Application.Features.ClassLectures.Commands.Create;

public class CreatedClassLectureResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid LectureId { get; set; }
    public Guid StudentClassId { get; set; }
}