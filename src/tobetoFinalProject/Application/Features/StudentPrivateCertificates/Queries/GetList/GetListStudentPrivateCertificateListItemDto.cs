using Core.Application.Dtos;

namespace Application.Features.StudentPrivateCertificates.Queries.GetList;

public class GetListStudentPrivateCertificateListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string? CertificateUrl { get; set; }
    public string? CertificateName { get; set; }
}