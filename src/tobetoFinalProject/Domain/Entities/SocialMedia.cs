using Core.Persistence.Repositories;

namespace Domain.Entities;

public class SocialMedia : Entity<Guid>
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }
}



