using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentSocialMedia : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public Guid SocialMediaId { get; set; }
    public string MediaAccountUrl { get; set; }
    public virtual SocialMedia? SocialMedia { get; set; }
    public virtual Student? Student { get; set; }
}



