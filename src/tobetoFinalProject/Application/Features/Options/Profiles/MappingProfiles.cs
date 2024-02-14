using Application.Features.Options.Commands.Create;
using Application.Features.Options.Commands.Delete;
using Application.Features.Options.Commands.Update;
using Application.Features.Options.Queries.GetById;
using Application.Features.Options.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Options.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Option, CreateOptionCommand>().ReverseMap();
        CreateMap<Option, CreatedOptionResponse>().ReverseMap();
        CreateMap<Option, UpdateOptionCommand>().ReverseMap();
        CreateMap<Option, UpdatedOptionResponse>().ReverseMap();
        CreateMap<Option, DeleteOptionCommand>().ReverseMap();
        CreateMap<Option, DeletedOptionResponse>().ReverseMap();
        CreateMap<Option, GetByIdOptionResponse>().ReverseMap();
        CreateMap<Option, GetListOptionListItemDto>().ReverseMap();
        CreateMap<IPaginate<Option>, GetListResponse<GetListOptionListItemDto>>().ReverseMap();
    }
}