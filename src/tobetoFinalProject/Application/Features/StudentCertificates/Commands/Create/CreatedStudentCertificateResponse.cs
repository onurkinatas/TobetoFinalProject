using Core.Application.Responses;

namespace Application.Features.StudentCertificates.Commands.Create;

public class CreatedStudentCertificateResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid CertificateId { get; set; }
}