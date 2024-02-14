using Application.Features.QuestionOptions.Commands.Create;
using Application.Features.QuestionOptions.Commands.Delete;
using Application.Features.QuestionOptions.Commands.Update;
using Application.Features.QuestionOptions.Queries.GetById;
using Application.Features.QuestionOptions.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.QuestionOptions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<QuestionOption, CreateQuestionOptionCommand>().ReverseMap();
        CreateMap<QuestionOption, CreatedQuestionOptionResponse>().ReverseMap();
        CreateMap<QuestionOption, UpdateQuestionOptionCommand>().ReverseMap();
        CreateMap<QuestionOption, UpdatedQuestionOptionResponse>().ReverseMap();
        CreateMap<QuestionOption, DeleteQuestionOptionCommand>().ReverseMap();
        CreateMap<QuestionOption, DeletedQuestionOptionResponse>().ReverseMap();
        CreateMap<QuestionOption, GetByIdQuestionOptionResponse>().ReverseMap();
        CreateMap<QuestionOption, GetListQuestionOptionListItemDto>().ReverseMap();
        CreateMap<IPaginate<QuestionOption>, GetListResponse<GetListQuestionOptionListItemDto>>().ReverseMap();
    }
}