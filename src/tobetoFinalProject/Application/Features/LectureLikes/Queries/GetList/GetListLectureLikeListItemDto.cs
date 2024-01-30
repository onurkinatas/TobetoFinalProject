using Core.Application.Dtos;

namespace Application.Features.LectureLikes.Queries.GetList;

public class GetListLectureLikeListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public bool IsLiked { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public string StudentEmail { get; set; }
    public string LectureName { get; set; }
}