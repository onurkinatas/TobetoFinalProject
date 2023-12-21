using Application.Features.Tags.Commands.Create;
using Application.Features.Tags.Commands.Delete;
using Application.Features.Tags.Commands.Update;
using Application.Features.Tags.Queries.GetById;
using Application.Features.Tags.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Tags.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Tag, CreateTagCommand>().ReverseMap();
        CreateMap<Tag, CreatedTagResponse>().ReverseMap();
        CreateMap<Tag, UpdateTagCommand>().ReverseMap();
        CreateMap<Tag, UpdatedTagResponse>().ReverseMap();
        CreateMap<Tag, DeleteTagCommand>().ReverseMap();
        CreateMap<Tag, DeletedTagResponse>().ReverseMap();
        CreateMap<Tag, GetByIdTagResponse>().ReverseMap();
        CreateMap<Tag, GetListTagListItemDto>().ReverseMap();
        CreateMap<IPaginate<Tag>, GetListResponse<GetListTagListItemDto>>().ReverseMap();
    }
}