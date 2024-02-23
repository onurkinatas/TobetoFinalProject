using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class StudentQuizOption:Entity<int>
{
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public int? OptionId { get; set; }
    public Guid? StudentId { get; set; }
    
}