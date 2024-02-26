using Core.Application.Responses;

namespace Application.Features.StudentLectureComments.Commands.Create;

public class CreatedStudentLectureCommentResponse : IResponse
{
    public Guid LectureId { get; set; }
    public string Comment { get; set; }
}