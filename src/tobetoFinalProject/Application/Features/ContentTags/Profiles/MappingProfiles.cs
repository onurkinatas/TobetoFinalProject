using Application.Features.ContentTags.Commands.Create;
using Application.Features.ContentTags.Commands.Delete;
using Application.Features.ContentTags.Commands.Update;
using Application.Features.ContentTags.Queries.GetById;
using Application.Features.ContentTags.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContentTags.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentTag, CreateContentTagCommand>().ReverseMap();
        CreateMap<ContentTag, CreatedContentTagResponse>().ReverseMap();
        CreateMap<ContentTag, UpdateContentTagCommand>().ReverseMap();
        CreateMap<ContentTag, UpdatedContentTagResponse>().ReverseMap();
        CreateMap<ContentTag, DeleteContentTagCommand>().ReverseMap();
        CreateMap<ContentTag, DeletedContentTagResponse>().ReverseMap();
        CreateMap<ContentTag, GetByIdContentTagResponse>().ReverseMap();
        CreateMap<ContentTag, GetListContentTagListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentTag>, GetListResponse<GetListContentTagListItemDto>>().ReverseMap();
    }
}