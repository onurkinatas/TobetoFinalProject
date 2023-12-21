using Application.Features.Stages.Commands.Create;
using Application.Features.Stages.Commands.Delete;
using Application.Features.Stages.Commands.Update;
using Application.Features.Stages.Queries.GetById;
using Application.Features.Stages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Stages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Stage, CreateStageCommand>().ReverseMap();
        CreateMap<Stage, CreatedStageResponse>().ReverseMap();
        CreateMap<Stage, UpdateStageCommand>().ReverseMap();
        CreateMap<Stage, UpdatedStageResponse>().ReverseMap();
        CreateMap<Stage, DeleteStageCommand>().ReverseMap();
        CreateMap<Stage, DeletedStageResponse>().ReverseMap();
        CreateMap<Stage, GetByIdStageResponse>().ReverseMap();
        CreateMap<Stage, GetListStageListItemDto>().ReverseMap();
        CreateMap<IPaginate<Stage>, GetListResponse<GetListStageListItemDto>>().ReverseMap();
    }
}