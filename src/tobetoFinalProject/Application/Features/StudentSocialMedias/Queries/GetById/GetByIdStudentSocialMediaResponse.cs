using Core.Application.Responses;

namespace Application.Features.StudentSocialMedias.Queries.GetById;

public class GetByIdStudentSocialMediaResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid SocialMediaId { get; set; }
    public string SocialMediaName { get; set; }
    public string MediaAccountUrl { get; set; }
}