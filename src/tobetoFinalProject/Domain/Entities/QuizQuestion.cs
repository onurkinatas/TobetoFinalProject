using Core.Persistence.Repositories;

namespace Domain.Entities;

public class QuizQuestion : Entity<int>
{
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public virtual Quiz? Quiz { get; set; }
    public virtual Question? Question { get; set; }

}

