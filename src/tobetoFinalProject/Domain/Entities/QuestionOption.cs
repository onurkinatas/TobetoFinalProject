using Core.Persistence.Repositories;

namespace Domain.Entities;

public class QuestionOption : Entity<int>
{
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
    public virtual Option? Option { get; set; }
    public virtual Question? Question { get; set; }

}

