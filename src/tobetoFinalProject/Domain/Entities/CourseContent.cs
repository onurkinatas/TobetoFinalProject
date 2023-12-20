using Core.Persistence.Repositories;

namespace Domain.Entities;

public class CourseContent : Entity<Guid>
{
    public Guid CourseId { get; set; }
    public Guid ContentId { get; set; }
    public virtual Course? Course { get; set; }
    public virtual Content? Content { get; set; }
}


