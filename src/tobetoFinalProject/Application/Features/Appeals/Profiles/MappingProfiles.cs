using Application.Features.Appeals.Commands.Create;
using Application.Features.Appeals.Commands.Delete;
using Application.Features.Appeals.Commands.Update;
using Application.Features.Appeals.Queries.GetById;
using Application.Features.Appeals.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Appeals.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Appeal, CreateAppealCommand>().ReverseMap();
        CreateMap<Appeal, CreatedAppealResponse>().ReverseMap();
        CreateMap<Appeal, UpdateAppealCommand>().ReverseMap();
        CreateMap<Appeal, UpdatedAppealResponse>().ReverseMap();
        CreateMap<Appeal, DeleteAppealCommand>().ReverseMap();
        CreateMap<Appeal, DeletedAppealResponse>().ReverseMap();
        CreateMap<Appeal, GetByIdAppealResponse>().ReverseMap();
        CreateMap<Appeal, GetListAppealListItemDto>().ReverseMap();
        CreateMap<IPaginate<Appeal>, GetListResponse<GetListAppealListItemDto>>().ReverseMap();
    }
}