using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Option : Entity<int>
{
    public string Text { get; set; }
    public virtual ICollection<QuestionOption> QuestionOptions { get; set; }

}

