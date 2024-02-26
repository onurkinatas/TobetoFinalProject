using Core.Application.Dtos;

namespace Application.Features.StudentLectureComments.Queries.GetList;

public class GetListStudentLectureCommentListItemDto : IDto
{
    public int Id { get; set; }
    public Guid LectureId { get; set; }
    public Guid StudentId { get; set; }
    public string Comment { get; set; }
}