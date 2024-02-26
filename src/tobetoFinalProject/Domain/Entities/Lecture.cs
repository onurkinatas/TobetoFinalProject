using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Lecture : Entity<Guid>
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public string ImageUrl { get; set; }
    public double EstimatedDuration { get; set; }
    public Guid ManufacturerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public virtual Manufacturer? Manufacturer { get; set; }
    public virtual Category? Category { get; set; }
    public virtual ICollection<LectureCourse>? LectureCourses { get; set; }
    public virtual ICollection<ClassLecture>? ClassLectures { get; set; }
    public virtual ICollection<StudentLectureComment>? StudentLectureComments { get; set; }
}