using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentExperience : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public virtual Student? Student { get; set; }
    public string CompanyName { get; set; }
    public string Sector { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public Guid CityId { get; set; }
    public virtual City? City { get; set; }
}
