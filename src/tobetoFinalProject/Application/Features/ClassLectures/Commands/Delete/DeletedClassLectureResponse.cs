using Core.Application.Responses;

namespace Application.Features.ClassLectures.Commands.Delete;

public class DeletedClassLectureResponse : IResponse
{
    public Guid Id { get; set; }
}