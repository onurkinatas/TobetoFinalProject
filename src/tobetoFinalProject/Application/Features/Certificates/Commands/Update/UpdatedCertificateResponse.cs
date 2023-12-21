using Core.Application.Responses;

namespace Application.Features.Certificates.Commands.Update;

public class UpdatedCertificateResponse : IResponse
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; }
}