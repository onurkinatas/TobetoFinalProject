using Core.Persistence.Repositories;

namespace Domain.Entities;

public class LectureSpentTime : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public double SpentedTime { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Lecture? Lecture { get; set; }
}


