using Application.Features.StudentStages.Commands.Create;
using Application.Features.StudentStages.Commands.Delete;
using Application.Features.StudentStages.Commands.Update;
using Application.Features.StudentStages.Queries.GetById;
using Application.Features.StudentStages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.StudentStages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentStage, CreateStudentStageCommand>().ReverseMap();
        CreateMap<StudentStage, CreatedStudentStageResponse>().ReverseMap();
        CreateMap<StudentStage, UpdateStudentStageCommand>().ReverseMap();
        CreateMap<StudentStage, UpdatedStudentStageResponse>().ReverseMap();
        CreateMap<StudentStage, DeleteStudentStageCommand>().ReverseMap();
        CreateMap<StudentStage, DeletedStudentStageResponse>().ReverseMap();
        CreateMap<StudentStage, GetByIdStudentStageResponse>().ReverseMap();
        CreateMap<StudentStage, GetListStudentStageListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentStage>, GetListResponse<GetListStudentStageListItemDto>>().ReverseMap();

    }
}