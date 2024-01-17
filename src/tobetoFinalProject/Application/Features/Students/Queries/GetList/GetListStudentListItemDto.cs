using Application.Features.Appeals.Queries.GetList;
using Application.Features.Certificates.Queries.GetList;
using Application.Features.LanguageLevels.Queries.GetList;
using Application.Features.Skills.Queries.GetList;
using Application.Features.SocialMedias.Queries.GetList;
using Application.Features.StudentClasses.Queries.GetList;
using Application.Features.StudentEducations.Queries.GetList;
using Application.Features.StudentExperiences.Queries.GetList;
using Core.Application.Dtos;

namespace Application.Features.Students.Queries.GetList;

public class GetListStudentListItemDto : IDto
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string CityName { get; set; }
    public string DistrictName { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public string AdrressDetail { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public ICollection<GetListSocialMediaListItemDto>? SocialMedias { get; set; }
    public ICollection<GetListCertificateListItemDto>? Certificates { get; set; }
    public ICollection<GetListLanguageLevelListItemDto>? LanguageLevels { get; set; }
    public ICollection<GetListSkillListItemDto>? Skills { get; set; }
    public ICollection<GetListAppealListItemDto>? Appeals { get; set; }
    public ICollection<GetListStudentEducationListItemDto>? StudentEducations { get; set; }
    public ICollection<GetListStudentExperienceListItemDto>? StudentExperiences { get; set; }
    public ICollection<GetStudentClassesDto>? StudentClasses { get; set; }
}