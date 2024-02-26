using Core.Application.Responses;

namespace Application.Features.StudentLectureComments.Commands.Create;

public class CreatedStudentLectureCommentResponse : IResponse
{
    public int Id { get; set; }
    public Guid LectureId { get; set; }
    public Guid StudentId { get; set; }
    public string Comment { get; set; }
}