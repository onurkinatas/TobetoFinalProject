using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Course : Entity<Guid>
{
    public string Name { get; set; }
    public virtual ICollection<CourseContent>? CourseContents { get; set; }
    public virtual ICollection<LectureCourse>? LectureCourses { get; set; }
}


