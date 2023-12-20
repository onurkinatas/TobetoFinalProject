using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Certificate : Entity<Guid>
{
    public string ImageUrl { get; set; }
    public virtual ICollection<StudentCertificate>? StudentCertificates { get; set; }
}


