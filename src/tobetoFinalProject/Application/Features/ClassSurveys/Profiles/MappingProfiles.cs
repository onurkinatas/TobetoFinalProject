using Application.Features.ClassSurveys.Commands.Create;
using Application.Features.ClassSurveys.Commands.Delete;
using Application.Features.ClassSurveys.Commands.Update;
using Application.Features.ClassSurveys.Queries.GetById;
using Application.Features.ClassSurveys.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ClassSurveys.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ClassSurvey, CreateClassSurveyCommand>().ReverseMap();
        CreateMap<ClassSurvey, CreatedClassSurveyResponse>().ReverseMap();
        CreateMap<ClassSurvey, UpdateClassSurveyCommand>().ReverseMap();
        CreateMap<ClassSurvey, UpdatedClassSurveyResponse>().ReverseMap();
        CreateMap<ClassSurvey, DeleteClassSurveyCommand>().ReverseMap();
        CreateMap<ClassSurvey, DeletedClassSurveyResponse>().ReverseMap();
        CreateMap<ClassSurvey, GetByIdClassSurveyResponse>().ReverseMap();
        CreateMap<ClassSurvey, GetListClassSurveyListItemDto>().ReverseMap();
        CreateMap<IPaginate<ClassSurvey>, GetListResponse<GetListClassSurveyListItemDto>>().ReverseMap();
        CreateMap<ClassSurvey, GetListClassSurveyListItemDto>()
    .ForMember(dest => dest.SurveyUrl, opt => opt.MapFrom(src => src.Survey.SurveyUrl));
    }
}