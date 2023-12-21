using Core.Application.Responses;

namespace Application.Features.Certificates.Queries.GetById;

public class GetByIdCertificateResponse : IResponse
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; }
}