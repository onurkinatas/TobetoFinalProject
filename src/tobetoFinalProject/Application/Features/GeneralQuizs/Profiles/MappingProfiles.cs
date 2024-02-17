using Application.Features.GeneralQuizs.Commands.Create;
using Application.Features.GeneralQuizs.Commands.Delete;
using Application.Features.GeneralQuizs.Commands.Update;
using Application.Features.GeneralQuizs.Queries.GetById;
using Application.Features.GeneralQuizs.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.GeneralQuizs.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<GeneralQuiz, CreateGeneralQuizCommand>().ReverseMap();
        CreateMap<GeneralQuiz, CreatedGeneralQuizResponse>().ReverseMap();
        CreateMap<GeneralQuiz, UpdateGeneralQuizCommand>().ReverseMap();
        CreateMap<GeneralQuiz, UpdatedGeneralQuizResponse>().ReverseMap();
        CreateMap<GeneralQuiz, DeleteGeneralQuizCommand>().ReverseMap();
        CreateMap<GeneralQuiz, DeletedGeneralQuizResponse>().ReverseMap();
        CreateMap<GeneralQuiz, GetByIdGeneralQuizResponse>().ReverseMap();
        CreateMap<GeneralQuiz, GetListGeneralQuizListItemDto>().ReverseMap();
        CreateMap<IPaginate<GeneralQuiz>, GetListResponse<GetListGeneralQuizListItemDto>>().ReverseMap();
    }
}