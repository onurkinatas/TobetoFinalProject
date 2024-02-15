using Application.Features.Appeals.Queries.GetList;
using Application.Features.Certificates.Queries.GetList;
using Application.Features.LanguageLevels.Queries.GetList;
using Application.Features.Skills.Queries.GetList;
using Application.Features.SocialMedias.Queries.GetList;
using Application.Features.StudentClasses.Queries.GetList;
using Application.Features.StudentEducations.Queries.GetList;
using Application.Features.StudentExperiences.Queries.GetList;
using Application.Features.StudentLanguageLevels.Queries.GetList;
using Application.Features.StudentPrivateCertificates.Queries.GetList;
using Application.Features.Students.Queries.GetList;
using Application.Features.StudentSkills.Queries.GetList;
using Core.Application.Responses;

namespace Application.Features.Students.Queries.GetById;

public class GetByTokenStudentResponse : IResponse
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
    public string ProfilePhotoPath { get; set; }
    public DateTime BirthDate { get; set; }
    public string AddressDetail { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public string CityId { get; set; }
    public string DistrictId { get; set; }
    public ICollection<GetListSocialMediaListItemDto>? SocialMedias { get; set; }
    public ICollection<GetListCertificateListItemDto>? Certificates { get; set; }
    public ICollection<GetListStudentLanguageLevelListItemDto>? LanguageLevels { get; set; }
    public ICollection<GetListStudentSkillListItemDto>? Skills { get; set; }
    public ICollection<GetListAppealListItemDto>? Appeals { get; set; }
    public ICollection<GetListStudentEducationListItemDto>? StudentEducations { get; set; }
    public ICollection<GetListStudentExperienceListItemDto>? StudentExperiences { get; set; }
    public ICollection<GetStudentClassesDto>? StudentClasses { get; set; }
    public ICollection<GetListStudentPrivateCertificateListItemDto>? StudentPrivateCertificates { get; set; }
}