using Application.Features.LectureSpentTimes.Commands.Create;
using Application.Features.LectureSpentTimes.Commands.Delete;
using Application.Features.LectureSpentTimes.Commands.Update;
using Application.Features.LectureSpentTimes.Queries.GetById;
using Application.Features.LectureSpentTimes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.LectureSpentTimes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<LectureSpentTime, CreateLectureSpentTimeCommand>().ReverseMap();
        CreateMap<LectureSpentTime, CreatedLectureSpentTimeResponse>().ReverseMap();
        CreateMap<LectureSpentTime, UpdateLectureSpentTimeCommand>().ReverseMap();
        CreateMap<LectureSpentTime, UpdatedLectureSpentTimeResponse>().ReverseMap();
        CreateMap<LectureSpentTime, DeleteLectureSpentTimeCommand>().ReverseMap();
        CreateMap<LectureSpentTime, DeletedLectureSpentTimeResponse>().ReverseMap();
        CreateMap<LectureSpentTime, GetByIdLectureSpentTimeResponse>().ReverseMap();
        CreateMap<LectureSpentTime, GetListLectureSpentTimeListItemDto>().ReverseMap();
        CreateMap<IPaginate<LectureSpentTime>, GetListResponse<GetListLectureSpentTimeListItemDto>>().ReverseMap();
    }
}