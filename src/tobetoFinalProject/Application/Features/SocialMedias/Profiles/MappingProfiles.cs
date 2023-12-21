using Application.Features.SocialMedias.Commands.Create;
using Application.Features.SocialMedias.Commands.Delete;
using Application.Features.SocialMedias.Commands.Update;
using Application.Features.SocialMedias.Queries.GetById;
using Application.Features.SocialMedias.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.SocialMedias.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SocialMedia, CreateSocialMediaCommand>().ReverseMap();
        CreateMap<SocialMedia, CreatedSocialMediaResponse>().ReverseMap();
        CreateMap<SocialMedia, UpdateSocialMediaCommand>().ReverseMap();
        CreateMap<SocialMedia, UpdatedSocialMediaResponse>().ReverseMap();
        CreateMap<SocialMedia, DeleteSocialMediaCommand>().ReverseMap();
        CreateMap<SocialMedia, DeletedSocialMediaResponse>().ReverseMap();
        CreateMap<SocialMedia, GetByIdSocialMediaResponse>().ReverseMap();
        CreateMap<SocialMedia, GetListSocialMediaListItemDto>().ReverseMap();
        CreateMap<IPaginate<SocialMedia>, GetListResponse<GetListSocialMediaListItemDto>>().ReverseMap();
    }
}