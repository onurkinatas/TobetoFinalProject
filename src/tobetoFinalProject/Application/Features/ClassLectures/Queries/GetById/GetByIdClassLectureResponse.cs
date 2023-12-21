using Core.Application.Responses;

namespace Application.Features.ClassLectures.Queries.GetById;

public class GetByIdClassLectureResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid LectureId { get; set; }
    public Guid StudentClassId { get; set; }
}