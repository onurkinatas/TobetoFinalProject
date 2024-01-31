using Application.Features.StudentSkills.Commands.Create;
using Application.Features.StudentSkills.Commands.Delete;
using Application.Features.StudentSkills.Commands.Update;
using Application.Features.StudentSkills.Queries.GetById;
using Application.Features.StudentSkills.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.StudentLanguageLevels.Queries.GetList;

namespace Application.Features.StudentSkills.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentSkill, CreateStudentSkillCommand>().ReverseMap();
        CreateMap<StudentSkill, CreatedStudentSkillResponse>().ReverseMap();
        CreateMap<StudentSkill, UpdateStudentSkillCommand>().ReverseMap();
        CreateMap<StudentSkill, UpdatedStudentSkillResponse>().ReverseMap();
        CreateMap<StudentSkill, DeleteStudentSkillCommand>().ReverseMap();
        CreateMap<StudentSkill, DeletedStudentSkillResponse>().ReverseMap();
        CreateMap<StudentSkill, GetByIdStudentSkillResponse>().ReverseMap();
        CreateMap<StudentSkill, GetListStudentSkillListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentSkill>, GetListResponse<GetListStudentSkillListItemDto>>().ReverseMap();

        CreateMap<StudentSkill, GetListStudentSkillListItemDto>()
           .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.User.FirstName))
           .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.User.LastName))
           .ForMember(dest => dest.StudentEmail, opt => opt.MapFrom(src => src.Student.User.Email));
    }
}