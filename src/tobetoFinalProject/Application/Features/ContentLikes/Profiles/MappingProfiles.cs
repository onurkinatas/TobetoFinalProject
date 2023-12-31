using Application.Features.ContentLikes.Commands.Create;
using Application.Features.ContentLikes.Commands.Delete;
using Application.Features.ContentLikes.Commands.Update;
using Application.Features.ContentLikes.Queries.GetById;
using Application.Features.ContentLikes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContentLikes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentLike, CreateContentLikeCommand>().ReverseMap();
        CreateMap<ContentLike, CreatedContentLikeResponse>().ReverseMap();
        CreateMap<ContentLike, UpdateContentLikeCommand>().ReverseMap();
        CreateMap<ContentLike, UpdatedContentLikeResponse>().ReverseMap();
        CreateMap<ContentLike, DeleteContentLikeCommand>().ReverseMap();
        CreateMap<ContentLike, DeletedContentLikeResponse>().ReverseMap();
        CreateMap<ContentLike, GetByIdContentLikeResponse>().ReverseMap();
        CreateMap<ContentLike, GetListContentLikeListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentLike>, GetListResponse<GetListContentLikeListItemDto>>().ReverseMap();
    }
}