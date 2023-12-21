using Core.Application.Responses;

namespace Application.Features.SocialMedias.Commands.Delete;

public class DeletedSocialMediaResponse : IResponse
{
    public Guid Id { get; set; }
}