using Core.Application.Responses;

namespace Application.Features.StudentCertificates.Commands.Update;

public class UpdatedStudentCertificateResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid CertificateId { get; set; }
}