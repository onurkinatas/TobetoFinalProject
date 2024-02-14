using Application.Features.QuizQuestions.Commands.Create;
using Application.Features.QuizQuestions.Commands.Delete;
using Application.Features.QuizQuestions.Commands.Update;
using Application.Features.QuizQuestions.Queries.GetById;
using Application.Features.QuizQuestions.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.QuizQuestions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<QuizQuestion, CreateQuizQuestionCommand>().ReverseMap();
        CreateMap<QuizQuestion, CreatedQuizQuestionResponse>().ReverseMap();
        CreateMap<QuizQuestion, UpdateQuizQuestionCommand>().ReverseMap();
        CreateMap<QuizQuestion, UpdatedQuizQuestionResponse>().ReverseMap();
        CreateMap<QuizQuestion, DeleteQuizQuestionCommand>().ReverseMap();
        CreateMap<QuizQuestion, DeletedQuizQuestionResponse>().ReverseMap();
        CreateMap<QuizQuestion, GetByIdQuizQuestionResponse>().ReverseMap();
        CreateMap<QuizQuestion, GetListQuizQuestionListItemDto>().ReverseMap();
        CreateMap<IPaginate<QuizQuestion>, GetListResponse<GetListQuizQuestionListItemDto>>().ReverseMap();
    }
}