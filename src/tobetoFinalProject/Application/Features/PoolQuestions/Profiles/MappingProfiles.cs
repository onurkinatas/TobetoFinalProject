using Application.Features.PoolQuestions.Commands.Create;
using Application.Features.PoolQuestions.Commands.Delete;
using Application.Features.PoolQuestions.Commands.Update;
using Application.Features.PoolQuestions.Queries.GetById;
using Application.Features.PoolQuestions.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.PoolQuestions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PoolQuestion, CreatePoolQuestionCommand>().ReverseMap();
        CreateMap<PoolQuestion, CreatedPoolQuestionResponse>().ReverseMap();
        CreateMap<PoolQuestion, UpdatePoolQuestionCommand>().ReverseMap();
        CreateMap<PoolQuestion, UpdatedPoolQuestionResponse>().ReverseMap();
        CreateMap<PoolQuestion, DeletePoolQuestionCommand>().ReverseMap();
        CreateMap<PoolQuestion, DeletedPoolQuestionResponse>().ReverseMap();
        CreateMap<PoolQuestion, GetByIdPoolQuestionResponse>().ReverseMap();
        CreateMap<PoolQuestion, GetListPoolQuestionListItemDto>().ReverseMap();
        CreateMap<IPaginate<PoolQuestion>, GetListResponse<GetListPoolQuestionListItemDto>>().ReverseMap();
    }
}