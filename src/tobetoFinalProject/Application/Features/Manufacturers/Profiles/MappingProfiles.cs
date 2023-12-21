using Application.Features.Manufacturers.Commands.Create;
using Application.Features.Manufacturers.Commands.Delete;
using Application.Features.Manufacturers.Commands.Update;
using Application.Features.Manufacturers.Queries.GetById;
using Application.Features.Manufacturers.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Manufacturers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Manufacturer, CreateManufacturerCommand>().ReverseMap();
        CreateMap<Manufacturer, CreatedManufacturerResponse>().ReverseMap();
        CreateMap<Manufacturer, UpdateManufacturerCommand>().ReverseMap();
        CreateMap<Manufacturer, UpdatedManufacturerResponse>().ReverseMap();
        CreateMap<Manufacturer, DeleteManufacturerCommand>().ReverseMap();
        CreateMap<Manufacturer, DeletedManufacturerResponse>().ReverseMap();
        CreateMap<Manufacturer, GetByIdManufacturerResponse>().ReverseMap();
        CreateMap<Manufacturer, GetListManufacturerListItemDto>().ReverseMap();
        CreateMap<IPaginate<Manufacturer>, GetListResponse<GetListManufacturerListItemDto>>().ReverseMap();
    }
}