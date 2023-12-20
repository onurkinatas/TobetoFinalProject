using Core.Persistence.Repositories;

namespace Domain.Entities;

public class LectureFavourite : Entity<Guid>
{
    public bool IsFavourited { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Lecture? Lecture { get; set; }
}


