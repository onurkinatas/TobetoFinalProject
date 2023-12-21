using Core.Application.Dtos;

namespace Application.Features.Certificates.Queries.GetList;

public class GetListCertificateListItemDto : IDto
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; }
}