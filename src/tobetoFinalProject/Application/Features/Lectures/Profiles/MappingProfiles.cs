using Application.Features.Lectures.Commands.Create;
using Application.Features.Lectures.Commands.Delete;
using Application.Features.Lectures.Commands.Update;
using Application.Features.Lectures.Queries.GetById;
using Application.Features.Lectures.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using System.Reflection;

namespace Application.Features.Lectures.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Lecture, CreateLectureCommand>().ReverseMap();
        CreateMap<Lecture, CreatedLectureResponse>().ReverseMap();
        CreateMap<Lecture, UpdateLectureCommand>().ReverseMap();
        CreateMap<Lecture, UpdatedLectureResponse>().ReverseMap();
        CreateMap<Lecture, DeleteLectureCommand>().ReverseMap();
        CreateMap<Lecture, DeletedLectureResponse>().ReverseMap();
        CreateMap<Lecture, GetByIdLectureResponse>().ReverseMap();
        CreateMap<Lecture, GetListLectureListItemDto>().ReverseMap();
        CreateMap<IPaginate<Lecture>, GetListResponse<GetListLectureListItemDto>>().ReverseMap();


        CreateMap<Lecture, GetListLectureListItemDto>()
            .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.LectureCourses.Select(si => si.Course).ToList()));
        CreateMap<Lecture, GetByIdLectureResponse>()
            .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.LectureCourses.Select(si => si.Course).ToList()));
    }
}