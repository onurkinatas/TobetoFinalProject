using Core.Application.Dtos;

namespace Application.Features.LectureViews.Queries.GetList;

public class GetListLectureViewListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public Guid ContentId { get; set; }
    public DateTime LectureViewCreatedDate { get; set; }
    public int? AllContentCountForStudent { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public string StudentEmail { get; set; }
    public string ContentName { get; set; }
    public string LectureName { get; set; }
}