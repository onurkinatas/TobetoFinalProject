using Application.Features.StudentQuizOptions.Commands.Create;
using Application.Features.StudentQuizOptions.Commands.Delete;
using Application.Features.StudentQuizOptions.Commands.Update;
using Application.Features.StudentQuizOptions.Queries.GetById;
using Application.Features.StudentQuizOptions.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.StudentQuizOptions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentQuizOption, CreateStudentQuizOptionCommand>().ReverseMap();
        CreateMap<StudentQuizOption, CreatedStudentQuizOptionResponse>().ReverseMap();
        CreateMap<StudentQuizOption, UpdateStudentQuizOptionCommand>().ReverseMap();
        CreateMap<StudentQuizOption, UpdatedStudentQuizOptionResponse>().ReverseMap();
        CreateMap<StudentQuizOption, DeleteStudentQuizOptionCommand>().ReverseMap();
        CreateMap<StudentQuizOption, DeletedStudentQuizOptionResponse>().ReverseMap();
        CreateMap<StudentQuizOption, GetByIdStudentQuizOptionResponse>().ReverseMap();
        CreateMap<StudentQuizOption, GetListStudentQuizOptionListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentQuizOption>, GetListResponse<GetListStudentQuizOptionListItemDto>>().ReverseMap();
    }
}