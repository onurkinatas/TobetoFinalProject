using Application.Features.StudentLanguageLevels.Commands.Create;
using Application.Features.StudentLanguageLevels.Commands.Delete;
using Application.Features.StudentLanguageLevels.Commands.Update;
using Application.Features.StudentLanguageLevels.Queries.GetById;
using Application.Features.StudentLanguageLevels.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.StudentLanguageLevels.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentLanguageLevel, CreateStudentLanguageLevelCommand>().ReverseMap();
        CreateMap<StudentLanguageLevel, CreatedStudentLanguageLevelResponse>().ReverseMap();
        CreateMap<StudentLanguageLevel, UpdateStudentLanguageLevelCommand>().ReverseMap();
        CreateMap<StudentLanguageLevel, UpdatedStudentLanguageLevelResponse>().ReverseMap();
        CreateMap<StudentLanguageLevel, DeleteStudentLanguageLevelCommand>().ReverseMap();
        CreateMap<StudentLanguageLevel, DeletedStudentLanguageLevelResponse>().ReverseMap();
        CreateMap<StudentLanguageLevel, GetByIdStudentLanguageLevelResponse>().ReverseMap();
        CreateMap<StudentLanguageLevel, GetListStudentLanguageLevelListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentLanguageLevel>, GetListResponse<GetListStudentLanguageLevelListItemDto>>().ReverseMap();


        CreateMap<StudentLanguageLevel, GetListStudentLanguageLevelListItemDto>()
            .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.LanguageLevel.LanguageId))
            .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.LanguageLevel.Language.Name));


        CreateMap<StudentLanguageLevel, GetByIdStudentLanguageLevelResponse>()
            .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.LanguageLevel.LanguageId))
            .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.LanguageLevel.Language.Name));
    }
}