using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentCertificate : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public Guid CertificateId { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Certificate? Certificate { get; set; }
}


