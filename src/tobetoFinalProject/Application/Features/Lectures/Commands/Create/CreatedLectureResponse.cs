using Core.Application.Responses;

namespace Application.Features.Lectures.Commands.Create;

public class CreatedLectureResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public string ImageUrl { get; set; }
    public double EstimatedDuration { get; set; }
    public Guid ManufacturerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}