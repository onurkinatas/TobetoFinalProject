using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class StudentExam : Entity<Guid>
{
    public Guid ExamId { get; set; }
    public Guid StudentId { get; set; }
    public virtual Exam? Exam { get; set; }
    public virtual Student Student { get; set; }
}
