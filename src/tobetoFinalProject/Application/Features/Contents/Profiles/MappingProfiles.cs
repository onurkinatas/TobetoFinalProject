using Application.Features.Contents.Commands.Create;
using Application.Features.Contents.Commands.Delete;
using Application.Features.Contents.Commands.Update;
using Application.Features.Contents.Queries.GetById;
using Application.Features.Contents.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Contents.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Content, CreateContentCommand>().ReverseMap();
        CreateMap<Content, CreatedContentResponse>().ReverseMap();
        CreateMap<Content, UpdateContentCommand>().ReverseMap();
        CreateMap<Content, UpdatedContentResponse>().ReverseMap();
        CreateMap<Content, DeleteContentCommand>().ReverseMap();
        CreateMap<Content, DeletedContentResponse>().ReverseMap();
        CreateMap<Content, GetByIdContentResponse>().ReverseMap();
        CreateMap<Content, GetListContentListItemDto>().ReverseMap();
        CreateMap<IPaginate<Content>, GetListResponse<GetListContentListItemDto>>().ReverseMap();

        CreateMap<Content, GetListContentListItemDto>()
            .ForMember(dest => dest.Instructors, opt => opt.MapFrom(src => src.ContentInstructors.Select(si => si.Instructor).ToList()));
        CreateMap<Content, GetByIdContentResponse>()
            .ForMember(dest => dest.Instructors, opt => opt.MapFrom(src => src.ContentInstructors.Select(si => si.Instructor).ToList()));
    }
}