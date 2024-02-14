using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Question : Entity<int>
{
    public string? ImageUrl { get; set; }
    public string Sentence { get; set; }
    public int CorrectOptionId { get; set; }
    public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
    public virtual ICollection<QuizQuestion> QuizQuestion { get; set; }

}

