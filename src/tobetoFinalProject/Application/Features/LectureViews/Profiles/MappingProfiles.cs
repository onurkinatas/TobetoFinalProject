using Application.Features.LectureViews.Commands.Create;
using Application.Features.LectureViews.Commands.Delete;
using Application.Features.LectureViews.Commands.Update;
using Application.Features.LectureViews.Queries.GetById;
using Application.Features.LectureViews.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.ContentLikes.Queries.GetList;

namespace Application.Features.LectureViews.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<LectureView, CreateLectureViewCommand>().ReverseMap();
        CreateMap<LectureView, CreatedLectureViewResponse>().ReverseMap();
        CreateMap<LectureView, UpdateLectureViewCommand>().ReverseMap();
        CreateMap<LectureView, UpdatedLectureViewResponse>().ReverseMap();
        CreateMap<LectureView, DeleteLectureViewCommand>().ReverseMap();
        CreateMap<LectureView, DeletedLectureViewResponse>().ReverseMap();
        CreateMap<LectureView, GetByIdLectureViewResponse>().ReverseMap();
        CreateMap<LectureView, GetListLectureViewListItemDto>().ReverseMap();
        CreateMap<IPaginate<LectureView>, GetListResponse<GetListLectureViewListItemDto>>().ReverseMap();

        CreateMap<LectureView, GetListLectureViewListItemDto>()
          .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.User.FirstName))
          .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.User.LastName))
          .ForMember(dest => dest.StudentEmail, opt => opt.MapFrom(src => src.Student.User.Email))
          .ForMember(dest => dest.LectureViewCreatedDate, opt => opt.MapFrom(src => src.CreatedDate));
    }
}