using Core.Application.Responses;

namespace Application.Features.StudentLectureComments.Commands.Delete;

public class DeletedStudentLectureCommentResponse : IResponse
{
    public int Id { get; set; }
}