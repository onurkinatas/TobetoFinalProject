using Application.Features.Lectures.Queries.GetById;
using Application.Features.Lectures.Queries.GetList;
using Core.Application.Dtos;

namespace Application.Features.LectureLikes.Queries.GetListForLoggedStudent;

public class GetListLectureLikeForLoggedStudentListItemDto : IDto
{
    public Guid Id { get; set; }
    public bool? IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public GetByIdLectureResponse? Lecture { get; set; }
}