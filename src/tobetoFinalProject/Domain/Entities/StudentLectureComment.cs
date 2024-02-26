using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class StudentLectureComment:Entity<int>
{
    public Guid LectureId { get; set; }
    public Guid StudentId { get; set; }
    public string Comment { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Lecture? Lecture { get; set; }
    public virtual ICollection<CommentSubComment>? CommentSubComments { get; set; }

}

