using Application.Features.StudentQuizResults.Commands.Create;
using Application.Features.StudentQuizResults.Commands.Delete;
using Application.Features.StudentQuizResults.Commands.Update;
using Application.Features.StudentQuizResults.Queries.GetById;
using Application.Features.StudentQuizResults.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.StudentQuizResults.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentQuizResult, CreateStudentQuizResultCommand>().ReverseMap();
        CreateMap<StudentQuizResult, CreatedStudentQuizResultResponse>().ReverseMap();
        CreateMap<StudentQuizResult, UpdateStudentQuizResultCommand>().ReverseMap();
        CreateMap<StudentQuizResult, UpdatedStudentQuizResultResponse>().ReverseMap();
        CreateMap<StudentQuizResult, DeleteStudentQuizResultCommand>().ReverseMap();
        CreateMap<StudentQuizResult, DeletedStudentQuizResultResponse>().ReverseMap();
        CreateMap<StudentQuizResult, GetByIdStudentQuizResultResponse>().ReverseMap();
        CreateMap<StudentQuizResult, GetListStudentQuizResultListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentQuizResult>, GetListResponse<GetListStudentQuizResultListItemDto>>().ReverseMap();
    }
}