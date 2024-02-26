using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;

public class CommentSubComment : Entity<int>
{
    public int StudentLectureCommentId { get; set; }
    public Guid StudentId { get; set; }
    public string SubComment { get; set; }
    public virtual Student? Student { get; set; }
    public virtual StudentLectureComment? StudentLectureComment { get; set; }
}

