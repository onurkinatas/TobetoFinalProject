using Core.Persistence.Repositories;

namespace Domain.Entities;

public class LectureCourse : Entity<Guid>
{
    public Guid CourseId { get; set; }
    public Guid LectureId { get; set; }
    public virtual Course? Course { get; set; }
    public virtual Lecture? Lecture { get; set; }
}