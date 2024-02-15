using Application.Features.Pools.Commands.Create;
using Application.Features.Pools.Commands.Delete;
using Application.Features.Pools.Commands.Update;
using Application.Features.Pools.Queries.GetById;
using Application.Features.Pools.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Pools.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Pool, CreatePoolCommand>().ReverseMap();
        CreateMap<Pool, CreatedPoolResponse>().ReverseMap();
        CreateMap<Pool, UpdatePoolCommand>().ReverseMap();
        CreateMap<Pool, UpdatedPoolResponse>().ReverseMap();
        CreateMap<Pool, DeletePoolCommand>().ReverseMap();
        CreateMap<Pool, DeletedPoolResponse>().ReverseMap();
        CreateMap<Pool, GetByIdPoolResponse>().ReverseMap();
        CreateMap<Pool, GetListPoolListItemDto>().ReverseMap();
        CreateMap<IPaginate<Pool>, GetListResponse<GetListPoolListItemDto>>().ReverseMap();
    }
}