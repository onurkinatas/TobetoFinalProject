using Core.Application.Responses;

namespace Application.Features.SocialMedias.Queries.GetById;

public class GetByIdSocialMediaResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
}