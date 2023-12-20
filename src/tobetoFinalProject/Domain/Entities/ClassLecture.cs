using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ClassLecture : Entity<Guid>
{
    public Guid LectureId { get; set; }
    public Guid StudentClassId { get; set; }
    public virtual Lecture? Lecture { get; set; }
    public virtual StudentClass? StudentClass { get; set; }

}


