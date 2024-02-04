using Core.Application.Dtos;

namespace Application.Features.StudentSocialMedias.Queries.GetList;

public class GetListStudentSocialMediaListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid SocialMediaId { get; set; }
    public string SocialMediaName { get; set; }
    public string MediaAccountUrl { get; set; }
    public string SocialMediaLogoUrl { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public string StudentEmail { get; set; }
}