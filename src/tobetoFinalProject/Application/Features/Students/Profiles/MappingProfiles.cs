using Application.Features.Students.Commands.Create;
using Application.Features.Students.Commands.Delete;
using Application.Features.Students.Commands.Update;
using Application.Features.Students.Queries.GetById;
using Application.Features.Students.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

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
        CreateMap<Student, GetListStudentListItemDto>().ReverseMap();
        CreateMap<StudentClass, GetStudentClassesDto>();
        CreateMap<IPaginate<Student>, GetListResponse<GetListStudentListItemDto>>().ReverseMap();


        CreateMap<Student, GetListStudentListItemDto>()
             .ForMember(dest => dest.Certificates, opt => opt.MapFrom(src => src.StudentCertificates.Select(si => si.Certificate).ToList()))
             .ForMember(dest => dest.Appeals, opt => opt.MapFrom(src => src.StudentAppeal.Select(si => si.Appeal).ToList()))
             .ForMember(dest => dest.LanguageLevels, opt => opt.MapFrom(src => src.StudentLanguageLevels.Select(si => si.LanguageLevel).ToList()))
             .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.StudentSkills.Select(si => si.Skill).ToList()))
             .ForMember(dest => dest.SocialMedias, opt => opt.MapFrom(src => src.StudentSocialMedias.Select(si => si.SocialMedia).ToList()))
             .ForMember(dest => dest.StudentClasses, opt => opt.MapFrom(src => src.StudentClassStudentes.Select(si => si.StudentClass).ToList()));


        CreateMap<Student, GetByIdStudentResponse>()
            .ForMember(dest => dest.Certificates, opt => opt.MapFrom(src => src.StudentCertificates.Select(si => si.Certificate).ToList()))
            .ForMember(dest => dest.Appeals, opt => opt.MapFrom(src => src.StudentAppeal.Select(si => si.Appeal).ToList()))
            .ForMember(dest => dest.LanguageLevels, opt => opt.MapFrom(src => src.StudentLanguageLevels.Select(si => si.LanguageLevel).ToList()))
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.StudentSkills.Select(si => si.Skill).ToList()))
            .ForMember(dest => dest.SocialMedias, opt => opt.MapFrom(src => src.StudentSocialMedias.Select(si => si.SocialMedia).ToList()))
            .ForMember(dest => dest.StudentClasses, opt => opt.MapFrom(src => src.StudentClassStudentes.Select(si => si.StudentClass).ToList()));
    }
}