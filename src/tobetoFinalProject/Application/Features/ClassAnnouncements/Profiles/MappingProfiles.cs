using Application.Features.ClassAnnouncements.Commands.Create;
using Application.Features.ClassAnnouncements.Commands.Delete;
using Application.Features.ClassAnnouncements.Commands.Update;
using Application.Features.ClassAnnouncements.Queries.GetById;
using Application.Features.ClassAnnouncements.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.ClassAnnouncements.Queries.GetListForLoggedStudent;

namespace Application.Features.ClassAnnouncements.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ClassAnnouncement, CreateClassAnnouncementCommand>().ReverseMap();
        CreateMap<ClassAnnouncement, CreatedClassAnnouncementResponse>().ReverseMap();
        CreateMap<ClassAnnouncement, UpdateClassAnnouncementCommand>().ReverseMap();
        CreateMap<ClassAnnouncement, UpdatedClassAnnouncementResponse>().ReverseMap();
        CreateMap<ClassAnnouncement, DeleteClassAnnouncementCommand>().ReverseMap();
        CreateMap<ClassAnnouncement, DeletedClassAnnouncementResponse>().ReverseMap();
        CreateMap<ClassAnnouncement, GetByIdClassAnnouncementResponse>().ReverseMap();
        CreateMap<ClassAnnouncement, GetListClassAnnouncementListItemDto>().ReverseMap();
        CreateMap<IPaginate<ClassAnnouncement>, GetListResponse<GetListClassAnnouncementListItemDto>>().ReverseMap();
        CreateMap<ClassAnnouncement, GetListForLoggedStudentClassAnnouncementListItemDto>().ReverseMap();
        CreateMap<IPaginate<ClassAnnouncement>, GetListResponse<GetListForLoggedStudentClassAnnouncementListItemDto>>().ReverseMap();
    }
}