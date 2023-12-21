using Application.Features.SubTypes.Commands.Create;
using Application.Features.SubTypes.Commands.Delete;
using Application.Features.SubTypes.Commands.Update;
using Application.Features.SubTypes.Queries.GetById;
using Application.Features.SubTypes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.SubTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SubType, CreateSubTypeCommand>().ReverseMap();
        CreateMap<SubType, CreatedSubTypeResponse>().ReverseMap();
        CreateMap<SubType, UpdateSubTypeCommand>().ReverseMap();
        CreateMap<SubType, UpdatedSubTypeResponse>().ReverseMap();
        CreateMap<SubType, DeleteSubTypeCommand>().ReverseMap();
        CreateMap<SubType, DeletedSubTypeResponse>().ReverseMap();
        CreateMap<SubType, GetByIdSubTypeResponse>().ReverseMap();
        CreateMap<SubType, GetListSubTypeListItemDto>().ReverseMap();
        CreateMap<IPaginate<SubType>, GetListResponse<GetListSubTypeListItemDto>>().ReverseMap();
    }
}