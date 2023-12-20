using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;

public class Student : Entity<Guid>
{
    public int UserId { get; set; }
    public Guid? CityId { get; set; }
    public Guid? DistrictId { get; set; }
    public string? NationalIdentity { get; set; }
    public string? Phone { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? AddressDetail { get; set; }
    public string? Description { get; set; }
    public string? ProfilePhotoPath { get; set; }
    public string? Country { get; set; }
    public virtual User? User { get; set; }
    public virtual City? City { get; set; }
    public virtual District? District { get; set; }
    public virtual ICollection<StudentEducation>? StudentEducations { get; set; }
    public virtual ICollection<StudentExperience>? StudentExperiences { get; set; }
    public virtual ICollection<StudentClassStudent>? StudentClassStudentes { get; set; }
    public virtual ICollection<StudentLanguageLevel>? StudentLanguageLevels { get; set; }
    public virtual ICollection<StudentSkill>? StudentSkills { get; set; }
    public virtual ICollection<StudentSocialMedia>? StudentSocialMedias { get; set; }
    public virtual ICollection<StudentCertificate>? StudentCertificates { get; set; }
    public virtual ICollection<StudentAppeal>? StudentAppeal { get; set; }
    public virtual ICollection<StudentStage>? StudentStages { get; set; }
}

