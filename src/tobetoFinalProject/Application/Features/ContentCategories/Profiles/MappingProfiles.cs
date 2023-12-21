using Application.Features.ContentCategories.Commands.Create;
using Application.Features.ContentCategories.Commands.Delete;
using Application.Features.ContentCategories.Commands.Update;
using Application.Features.ContentCategories.Queries.GetById;
using Application.Features.ContentCategories.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContentCategories.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentCategory, CreateContentCategoryCommand>().ReverseMap();
        CreateMap<ContentCategory, CreatedContentCategoryResponse>().ReverseMap();
        CreateMap<ContentCategory, UpdateContentCategoryCommand>().ReverseMap();
        CreateMap<ContentCategory, UpdatedContentCategoryResponse>().ReverseMap();
        CreateMap<ContentCategory, DeleteContentCategoryCommand>().ReverseMap();
        CreateMap<ContentCategory, DeletedContentCategoryResponse>().ReverseMap();
        CreateMap<ContentCategory, GetByIdContentCategoryResponse>().ReverseMap();
        CreateMap<ContentCategory, GetListContentCategoryListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentCategory>, GetListResponse<GetListContentCategoryListItemDto>>().ReverseMap();
    }
}