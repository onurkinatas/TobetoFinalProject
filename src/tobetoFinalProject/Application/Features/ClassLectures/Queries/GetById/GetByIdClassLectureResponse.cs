using Core.Application.Responses;

namespace Application.Features.ClassLectures.Queries.GetById;

public class GetByIdClassLectureResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid LectureId { get; set; }
    public Guid StudentClassId { get; set; }
    public string StudentClassName { get; set; }
    public string LectureName { get; set; }
    public string LectureCategoryName { get; set; }
    public string LectureImageUrl { get; set; }
    public double EstimatedVideoDuration { get; set; }
    public string LectureManufacturerName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}