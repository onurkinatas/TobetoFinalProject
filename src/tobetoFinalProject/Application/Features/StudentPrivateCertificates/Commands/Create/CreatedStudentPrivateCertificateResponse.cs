using Core.Application.Responses;

namespace Application.Features.StudentPrivateCertificates.Commands.Create;

public class CreatedStudentPrivateCertificateResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string? CertificateUrl { get; set; }
}