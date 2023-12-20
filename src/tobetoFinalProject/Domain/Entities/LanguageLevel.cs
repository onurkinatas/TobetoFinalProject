using Core.Persistence.Repositories;

namespace Domain.Entities;

public class LanguageLevel : Entity<Guid>
{
    public Guid LanguageId { get; set; }
    public string Name { get; set; }
    public virtual Language? Language { get; set; }
    public virtual ICollection<StudentLanguageLevel>? StudentLanguageLevels { get; set; }
}


