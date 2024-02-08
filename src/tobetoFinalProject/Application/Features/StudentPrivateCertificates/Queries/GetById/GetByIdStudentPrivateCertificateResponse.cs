using Core.Application.Responses;

namespace Application.Features.StudentPrivateCertificates.Queries.GetById;

public class GetByIdStudentPrivateCertificateResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string? CertificateUrl { get; set; }
}