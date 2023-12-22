using Core.Application.Responses;

namespace Application.Features.StudentCertificates.Queries.GetById;

public class GetByIdStudentCertificateResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid CertificateId { get; set; }
    public string CertificateImageUrl { get; set; }
}