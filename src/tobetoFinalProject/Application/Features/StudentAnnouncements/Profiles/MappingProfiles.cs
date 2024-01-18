using Application.Features.StudentAnnouncements.Commands.Create;
using Application.Features.StudentAnnouncements.Commands.Delete;
using Application.Features.StudentAnnouncements.Commands.Update;
using Application.Features.StudentAnnouncements.Queries.GetById;
using Application.Features.StudentAnnouncements.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.StudentAnnouncements.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentAnnouncement, CreateStudentAnnouncementCommand>().ReverseMap();
        CreateMap<StudentAnnouncement, CreatedStudentAnnouncementResponse>().ReverseMap();
        CreateMap<StudentAnnouncement, UpdateStudentAnnouncementCommand>().ReverseMap();
        CreateMap<StudentAnnouncement, UpdatedStudentAnnouncementResponse>().ReverseMap();
        CreateMap<StudentAnnouncement, DeleteStudentAnnouncementCommand>().ReverseMap();
        CreateMap<StudentAnnouncement, DeletedStudentAnnouncementResponse>().ReverseMap();
        CreateMap<StudentAnnouncement, GetByIdStudentAnnouncementResponse>().ReverseMap();
        CreateMap<StudentAnnouncement, GetListStudentAnnouncementListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentAnnouncement>, GetListResponse<GetListStudentAnnouncementListItemDto>>().ReverseMap();
        CreateMap<StudentAnnouncement, GetListStudentAnnouncementListItemDto>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Announcement.Name)) // Name özelliðini maple
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Announcement.Description)) // Description özelliðini maple
        .ReverseMap();

        CreateMap<StudentAnnouncement, GetByIdStudentAnnouncementResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Announcement.Name)) // Name özelliðini maple
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Announcement.Description)) // Description özelliðini maple
            .ReverseMap();
    }
}