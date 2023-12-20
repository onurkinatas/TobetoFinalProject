using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Skill : Entity<Guid>
{
    public string Name { get; set; }
    public virtual ICollection<StudentSkill>? StudentSkills { get; set; }
}


