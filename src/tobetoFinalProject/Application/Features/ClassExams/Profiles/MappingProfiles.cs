using Application.Features.ClassExams.Commands.Create;
using Application.Features.ClassExams.Commands.Delete;
using Application.Features.ClassExams.Commands.Update;
using Application.Features.ClassExams.Queries.GetById;
using Application.Features.ClassExams.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ClassExams.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ClassExam, CreateClassExamCommand>().ReverseMap();
        CreateMap<ClassExam, CreatedClassExamResponse>().ReverseMap();
        CreateMap<ClassExam, UpdateClassExamCommand>().ReverseMap();
        CreateMap<ClassExam, UpdatedClassExamResponse>().ReverseMap();
        CreateMap<ClassExam, DeleteClassExamCommand>().ReverseMap();
        CreateMap<ClassExam, DeletedClassExamResponse>().ReverseMap();
        CreateMap<ClassExam, GetByIdClassExamResponse>().ReverseMap();
        CreateMap<ClassExam, GetListClassExamListItemDto>().ReverseMap();
        CreateMap<IPaginate<ClassExam>, GetListResponse<GetListClassExamListItemDto>>().ReverseMap();
    }
}