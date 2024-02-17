using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ClassQuiz : Entity<int>
{
    public Guid StudentClassId { get; set; }
    public int QuizId { get; set; }
    public virtual Quiz? Quiz { get; set; }
    public virtual StudentClass? StudentClass { get; set; }

}
