using Core.Application.Responses;

namespace Application.Features.Students.Commands.Update;

public class UpdatedStudentResponse : IResponse
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public Guid? CityId { get; set; }
    public Guid? DistrictId { get; set; }
    public string? NationalIdentity { get; set; }
    public string? Phone { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? AddressDetail { get; set; }
    public string? Description { get; set; }
    public string? ProfilePhotoPath { get; set; }
    public string? Country { get; set; }
}