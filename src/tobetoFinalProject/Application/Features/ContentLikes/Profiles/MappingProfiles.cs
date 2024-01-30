using Application.Features.ContentLikes.Commands.Create;
using Application.Features.ContentLikes.Commands.Delete;
using Application.Features.ContentLikes.Commands.Update;
using Application.Features.ContentLikes.Queries.GetById;
using Application.Features.ContentLikes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.ContentLikes.Queries.GetListForLoggedStudent;
using Application.Features.ContentLikes.Queries.GetByContentId;
using Application.Features.LectureLikes.Queries.GetList;

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
        CreateMap<ContentLike,GetListContentLikeForLoggedStudentListItemDto>().ReverseMap();
        CreateMap<ContentLike, GetByIdContentLikeResponse>().ReverseMap();
        CreateMap<ContentLike, GetByContentIdContentLikeResponse>().ReverseMap();
        CreateMap<ContentLike, GetListContentLikeListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentLike>, GetListResponse<GetListContentLikeListItemDto>>().ReverseMap();
        CreateMap<IPaginate<ContentLike>, GetListResponse<GetListContentLikeForLoggedStudentListItemDto>>().ReverseMap();

        CreateMap<ContentLike, GetListContentLikeListItemDto>()
          .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.User.FirstName))
          .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.User.LastName))
          .ForMember(dest => dest.StudentEmail, opt => opt.MapFrom(src => src.Student.User.Email));
    }
}