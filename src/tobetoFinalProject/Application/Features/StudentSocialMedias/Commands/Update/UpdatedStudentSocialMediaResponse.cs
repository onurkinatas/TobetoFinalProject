using Core.Application.Responses;

namespace Application.Features.StudentSocialMedias.Commands.Update;

public class UpdatedStudentSocialMediaResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid SocialMediaId { get; set; }
    public string MediaAccountUrl { get; set; }
}