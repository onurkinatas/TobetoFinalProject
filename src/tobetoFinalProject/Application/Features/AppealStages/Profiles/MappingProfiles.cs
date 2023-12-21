using Application.Features.AppealStages.Commands.Create;
using Application.Features.AppealStages.Commands.Delete;
using Application.Features.AppealStages.Commands.Update;
using Application.Features.AppealStages.Queries.GetById;
using Application.Features.AppealStages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.AppealStages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AppealStage, CreateAppealStageCommand>().ReverseMap();
        CreateMap<AppealStage, CreatedAppealStageResponse>().ReverseMap();
        CreateMap<AppealStage, UpdateAppealStageCommand>().ReverseMap();
        CreateMap<AppealStage, UpdatedAppealStageResponse>().ReverseMap();
        CreateMap<AppealStage, DeleteAppealStageCommand>().ReverseMap();
        CreateMap<AppealStage, DeletedAppealStageResponse>().ReverseMap();
        CreateMap<AppealStage, GetByIdAppealStageResponse>().ReverseMap();
        CreateMap<AppealStage, GetListAppealStageListItemDto>().ReverseMap();
        CreateMap<IPaginate<AppealStage>, GetListResponse<GetListAppealStageListItemDto>>().ReverseMap();
    }
}