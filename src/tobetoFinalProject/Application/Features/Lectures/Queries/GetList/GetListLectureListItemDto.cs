using Application.Features.Courses.Queries.GetList;
using Core.Application.Dtos;

namespace Application.Features.Lectures.Queries.GetList;

public class GetListLectureListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CategoryName { get; set; }
    public string ImageUrl { get; set; }
    public double EstimatedVideoDuration { get; set; }
    public string ManufacturerName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<GetListCourseListItemDto>? Courses { get; set; }
}