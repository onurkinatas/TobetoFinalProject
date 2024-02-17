using Core.Persistence.Repositories;

namespace Domain.Entities;

public class GeneralQuiz : Entity<int>
{
    public int QuizId { get; set; }
    public virtual Quiz? Quiz { get; set; }

}
