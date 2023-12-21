using Application.Features.LectureCourses.Commands.Create;
using Application.Features.LectureCourses.Commands.Delete;
using Application.Features.LectureCourses.Commands.Update;
using Application.Features.LectureCourses.Queries.GetById;
using Application.Features.LectureCourses.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.LectureCourses.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<LectureCourse, CreateLectureCourseCommand>().ReverseMap();
        CreateMap<LectureCourse, CreatedLectureCourseResponse>().ReverseMap();
        CreateMap<LectureCourse, UpdateLectureCourseCommand>().ReverseMap();
        CreateMap<LectureCourse, UpdatedLectureCourseResponse>().ReverseMap();
        CreateMap<LectureCourse, DeleteLectureCourseCommand>().ReverseMap();
        CreateMap<LectureCourse, DeletedLectureCourseResponse>().ReverseMap();
        CreateMap<LectureCourse, GetByIdLectureCourseResponse>().ReverseMap();
        CreateMap<LectureCourse, GetListLectureCourseListItemDto>().ReverseMap();
        CreateMap<IPaginate<LectureCourse>, GetListResponse<GetListLectureCourseListItemDto>>().ReverseMap();
    }
}