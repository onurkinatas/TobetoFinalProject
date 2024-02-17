using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Quiz:Entity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public virtual List<QuizQuestion> QuizQuestions { get; set; }

}
