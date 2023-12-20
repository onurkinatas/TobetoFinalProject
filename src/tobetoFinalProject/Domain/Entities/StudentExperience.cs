using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentExperience : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public Guid ExperienceId { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Experience? Experience { get; set; }
}



