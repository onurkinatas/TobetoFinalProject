using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class StudentQuizResult:Entity<int>
{
    public Guid StudentId { get; set; }
    public int QuizId { get; set; }
    public int? CorrectAnswerCount { get; set; }
    public int? WrongAnswerCount { get; set; }
    public int? EmptyAnswerCount { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Quiz? Quiz { get; set; }
}

