using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentSkill : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public Guid SkillId { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Skill? Skill { get; set; }
}


