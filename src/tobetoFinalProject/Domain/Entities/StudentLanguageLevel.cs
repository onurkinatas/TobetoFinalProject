using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentLanguageLevel : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public Guid LanguageLevelId { get; set; }
    public virtual Student? Student { get; set; }
    public virtual LanguageLevel? LanguageLevel { get; set; }
}


