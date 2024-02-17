using Application.Features.ClassQuizs.Commands.Create;
using Application.Features.ClassQuizs.Commands.Delete;
using Application.Features.ClassQuizs.Commands.Update;
using Application.Features.ClassQuizs.Queries.GetById;
using Application.Features.ClassQuizs.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ClassQuizs.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ClassQuiz, CreateClassQuizCommand>().ReverseMap();
        CreateMap<ClassQuiz, CreatedClassQuizResponse>().ReverseMap();
        CreateMap<ClassQuiz, UpdateClassQuizCommand>().ReverseMap();
        CreateMap<ClassQuiz, UpdatedClassQuizResponse>().ReverseMap();
        CreateMap<ClassQuiz, DeleteClassQuizCommand>().ReverseMap();
        CreateMap<ClassQuiz, DeletedClassQuizResponse>().ReverseMap();
        CreateMap<ClassQuiz, GetByIdClassQuizResponse>().ReverseMap();
        CreateMap<ClassQuiz, GetListClassQuizListItemDto>().ReverseMap();
        CreateMap<IPaginate<ClassQuiz>, GetListResponse<GetListClassQuizListItemDto>>().ReverseMap();
    }
}