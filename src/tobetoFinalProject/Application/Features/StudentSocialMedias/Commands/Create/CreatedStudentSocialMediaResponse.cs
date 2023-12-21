using Core.Application.Responses;

namespace Application.Features.StudentSocialMedias.Commands.Create;

public class CreatedStudentSocialMediaResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid SocialMediaId { get; set; }
    public string MediaAccountUrl { get; set; }
}