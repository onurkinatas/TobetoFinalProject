using Core.Application.Responses;

namespace Application.Features.SocialMedias.Commands.Update;

public class UpdatedSocialMediaResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
}