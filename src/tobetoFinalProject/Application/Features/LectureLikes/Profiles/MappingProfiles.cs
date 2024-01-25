using Application.Features.LectureLikes.Commands.Create;
using Application.Features.LectureLikes.Commands.Delete;
using Application.Features.LectureLikes.Commands.Update;
using Application.Features.LectureLikes.Queries.GetById;
using Application.Features.LectureLikes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.LectureLikes.Queries.GetByLectureId;
using Application.Features.LectureLikes.Queries.GetListForLoggedStudent;
using Application.Features.Lectures.Queries.GetById;
using Application.Features.Lectures.Queries.GetList;

namespace Application.Features.LectureLikes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<LectureLike, CreateLectureLikeCommand>().ReverseMap();
        CreateMap<LectureLike, CreatedLectureLikeResponse>().ReverseMap();
        CreateMap<LectureLike, UpdateLectureLikeCommand>().ReverseMap();
        CreateMap<LectureLike, UpdatedLectureLikeResponse>().ReverseMap();
        CreateMap<LectureLike, GetByLectureIdLectureLikeResponse>().ReverseMap();
        CreateMap<LectureLike, GetListLectureLikeForLoggedStudentListItemDto>().ReverseMap();
        CreateMap<LectureLike, DeleteLectureLikeCommand>().ReverseMap();
        CreateMap<LectureLike, DeletedLectureLikeResponse>().ReverseMap();
        CreateMap<LectureLike, GetByIdLectureLikeResponse>().ReverseMap();
        CreateMap<LectureLike, GetListLectureLikeListItemDto>().ReverseMap();
        CreateMap<IPaginate<LectureLike>, GetListResponse<GetListLectureLikeListItemDto>>().ReverseMap();
        CreateMap<IPaginate<LectureLike>, GetListResponse<GetListLectureLikeForLoggedStudentListItemDto>>().ReverseMap();


    }
}