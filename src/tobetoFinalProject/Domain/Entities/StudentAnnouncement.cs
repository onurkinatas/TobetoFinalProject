using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentAnnouncement : Entity<Guid>
{
    public Guid AnnouncementId { get; set; }
    public Guid StudentId { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Announcement? Announcement { get; set; }

}