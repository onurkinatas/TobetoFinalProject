using Application.Features.StudentLectureComments.Queries.Dtos;
using Core.Application.Responses;

namespace Application.Features.StudentLectureComments.Queries.GetById;

public class GetByIdStudentLectureCommentResponse : IResponse
{
    public int Id { get; set; }
    public string ProfilePhotoPath { get; set; }
    public Guid StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Comment { get; set; }
    public ICollection<StudentSubCommentDto>? CommentSubComments { get; set; }
    public bool IsDeletable { get; set; }
    public DateTime CreatedDate { get; set; }
}