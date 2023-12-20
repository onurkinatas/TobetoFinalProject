using Core.Persistence.Repositories;

namespace Domain.Entities;

public class LectureCompletionCondition : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public int CompletionPercentage { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Lecture? Lecture { get; set; }
    public virtual ICollection<LectureView> LectureViews { get; set;}
    
}