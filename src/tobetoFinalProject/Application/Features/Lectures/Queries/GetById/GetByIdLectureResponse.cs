using Application.Features.Courses.Queries.GetList;
using Core.Application.Responses;

namespace Application.Features.Lectures.Queries.GetById;

public class GetByIdLectureResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CategoryName { get; set; }
    public string ImageUrl { get; set; }
    public double EstimatedVideoDuration { get; set; }
    public string ManufacturerName { get; set; }
    public int LikeCount { get; set; }
    public bool IsLiked { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<GetListCourseListItemDto> Courses { get; set; }
}