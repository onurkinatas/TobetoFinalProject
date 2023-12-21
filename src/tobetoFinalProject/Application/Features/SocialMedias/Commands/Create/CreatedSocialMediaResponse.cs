using Core.Application.Responses;

namespace Application.Features.SocialMedias.Commands.Create;

public class CreatedSocialMediaResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
}