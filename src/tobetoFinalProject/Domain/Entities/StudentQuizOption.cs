using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class StudentQuizOption:Entity<int>
{
    public Guid ExamId { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public int OptionId { get; set; }
    public Guid? StudentId { get; set; }
    public virtual Quiz? Quiz { get; set; }
    public virtual Question? Question { get; set;}
    public virtual Exam? Exam { get; set; }
    public virtual Option? Option { get; set; }
    public virtual Student? Student { get; set; }
}