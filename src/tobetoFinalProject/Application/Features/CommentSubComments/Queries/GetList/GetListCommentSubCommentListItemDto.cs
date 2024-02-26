using Core.Application.Dtos;

namespace Application.Features.CommentSubComments.Queries.GetList;

public class GetListCommentSubCommentListItemDto : IDto
{
    public int Id { get; set; }
    public int UserLectureCommentId { get; set; }
    public Guid StudentId { get; set; }
    public string SubComment { get; set; }
}