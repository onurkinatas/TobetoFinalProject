using Core.Application.Responses;

namespace Application.Features.StudentLectureComments.Queries.GetById;

public class GetByIdStudentLectureCommentResponse : IResponse
{
    public int Id { get; set; }
    public Guid LectureId { get; set; }
    public Guid StudentId { get; set; }
    public string Comment { get; set; }
}