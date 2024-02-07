using Application.Features.ClassLectures.Commands.Create;
using Application.Features.ClassLectures.Commands.Delete;
using Application.Features.ClassLectures.Commands.Update;
using Application.Features.ClassLectures.Queries.GetById;
using Application.Features.ClassLectures.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.StudentAnnouncements.Queries.GetList;

namespace Application.Features.ClassLectures.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ClassLecture, CreateClassLectureCommand>().ReverseMap();
        CreateMap<ClassLecture, CreatedClassLectureResponse>().ReverseMap();
        CreateMap<ClassLecture, UpdateClassLectureCommand>().ReverseMap();
        CreateMap<ClassLecture, UpdatedClassLectureResponse>().ReverseMap();
        CreateMap<ClassLecture, DeleteClassLectureCommand>().ReverseMap();
        CreateMap<ClassLecture, DeletedClassLectureResponse>().ReverseMap();
        CreateMap<ClassLecture, GetByIdClassLectureResponse>().ReverseMap();
        CreateMap<ClassLecture, GetListClassLectureListItemDto>().ReverseMap();
        CreateMap<IPaginate<ClassLecture>, GetListResponse<GetListClassLectureListItemDto>>().ReverseMap();

        CreateMap<ClassLecture, GetListClassLectureListItemDto>()
       .ForMember(dest => dest.CompletionPercentage, opt => opt.MapFrom(src => src.LectureCompletionCondition.CompletionPercentage))
       ;

    }
}