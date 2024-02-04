using Application.Features.Students.Commands.Create;
using Application.Features.Students.Commands.Delete;
using Application.Features.Students.Commands.Update;
using Application.Features.Students.Queries.GetById;
using Application.Features.Students.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Application.Features.Students.Commands.UpdateForPassword;

namespace Application.Features.Students.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Student, CreateStudentCommand>().ReverseMap();
        CreateMap<Student, CreatedStudentResponse>().ReverseMap();
        CreateMap<Student, UpdateStudentCommand>().ReverseMap();
        CreateMap<Student, UpdatedStudentResponse>().ReverseMap();
        CreateMap<Student, DeleteStudentCommand>().ReverseMap();
        CreateMap<Student, DeletedStudentResponse>().ReverseMap();
        CreateMap<Student, GetByIdStudentResponse>().ReverseMap();
        CreateMap<Student, GetByTokenStudentResponse>().ReverseMap();
        CreateMap<Student, GetListStudentListItemDto>().ReverseMap();
        CreateMap<StudentClass, GetStudentClassesDto>();
        CreateMap<IPaginate<Student>, GetListResponse<GetListStudentListItemDto>>().ReverseMap();
        CreateMap<User, UpdateStudentForPasswordCommand>().ReverseMap();


        CreateMap<Student, GetListStudentListItemDto>()
             .ForMember(dest => dest.Certificates, opt => opt.MapFrom(src => src.StudentCertificates.Select(si => si.Certificate).ToList()))
             .ForMember(dest => dest.Appeals, opt => opt.MapFrom(src => src.StudentAppeal.Select(si => si.Appeal).ToList()))
             .ForMember(dest => dest.LanguageLevels, opt => opt.MapFrom(src => src.StudentLanguageLevels.Select(si => si.LanguageLevel).ToList()))
             .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.StudentSkills.Select(si => si.Skill).ToList()))
             .ForMember(dest => dest.SocialMedias, opt => opt.MapFrom(src => src.StudentSocialMedias.Select(si => si.SocialMedia).ToList()))
             .ForMember(dest => dest.StudentClasses, opt => opt.MapFrom(src => src.StudentClassStudentes.Select(si => si.StudentClass).ToList()))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
             .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));


        CreateMap<Student, GetByIdStudentResponse>()
            .ForMember(dest => dest.Certificates, opt => opt.MapFrom(src => src.StudentCertificates.Select(si => si.Certificate).ToList()))
            .ForMember(dest => dest.Appeals, opt => opt.MapFrom(src => src.StudentAppeal.Select(si => si.Appeal).ToList()))
            .ForMember(dest => dest.LanguageLevels, opt => opt.MapFrom(src => src.StudentLanguageLevels.Select(si => si.LanguageLevel).ToList()))
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.StudentSkills.Select(si => si.Skill).ToList()))
            .ForMember(dest => dest.SocialMedias, opt => opt.MapFrom(src => src.StudentSocialMedias.Select(si => si.SocialMedia).ToList()))
            .ForMember(dest => dest.StudentClasses, opt => opt.MapFrom(src => src.StudentClassStudentes.Select(si => si.StudentClass).ToList()))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
             .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

        CreateMap<Student, GetByTokenStudentResponse>()
            .ForMember(dest => dest.Certificates, opt => opt.MapFrom(src => src.StudentCertificates.Select(si => si.Certificate).ToList()))
            .ForMember(dest => dest.Appeals, opt => opt.MapFrom(src => src.StudentAppeal.Select(si => si.Appeal).ToList()))
            .ForMember(dest => dest.LanguageLevels, opt => opt.MapFrom(src => src.StudentLanguageLevels.Select(si => si.LanguageLevel).ToList()))
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.StudentSkills.Select(si => si.Skill).ToList()))
            .ForMember(dest => dest.SocialMedias, opt => opt.MapFrom(src => src.StudentSocialMedias.Select(si => si.SocialMedia).ToList()))
            .ForMember(dest => dest.StudentClasses, opt => opt.MapFrom(src => src.StudentClassStudentes.Select(si => si.StudentClass).ToList()))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
             .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
    }
}