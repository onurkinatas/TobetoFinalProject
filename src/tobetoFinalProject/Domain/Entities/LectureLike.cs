using Core.Persistence.Repositories;

namespace Domain.Entities;

public class LectureLike : Entity<Guid>
{
    public bool IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Lecture? Lecture { get; set; }
}


