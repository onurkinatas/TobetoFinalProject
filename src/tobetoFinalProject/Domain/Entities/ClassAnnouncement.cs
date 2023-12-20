using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ClassAnnouncement : Entity<Guid>
{
    public Guid AnnouncementId { get; set; }
    public Guid StudentClassId { get; set; }
    public virtual Announcement? Announcement { get; set; }
    public virtual StudentClass? StudentClass { get; set; }

}


