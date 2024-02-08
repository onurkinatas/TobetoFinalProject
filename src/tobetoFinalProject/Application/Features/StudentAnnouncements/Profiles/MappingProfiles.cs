using Application.Features.StudentAnnouncements.Commands.Create;
using Application.Features.StudentAnnouncements.Commands.Delete;
using Application.Features.StudentAnnouncements.Commands.Update;
using Application.Features.StudentAnnouncements.Queries.GetById;
using Application.Features.StudentAnnouncements.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.StudentAnnouncements.Queries.GetList;
using Application.Features.StudentAnnouncements.Queries.GetListForLoggedStudent;
using Application.Features.StudentAnnouncements.Queries.GetListByAnnouncementId;

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
        CreateMap<StudentAnnouncement,GetListForLoggedStudentAnnouncementResponse>().ReverseMap();
        CreateMap<StudentAnnouncement, GetListByAnnouncementIdStudentAnnouncementResponse>().ReverseMap();
        CreateMap<IPaginate<StudentAnnouncement>, GetListResponse<GetListStudentAnnouncementListItemDto>>().ReverseMap();
        CreateMap<IPaginate<StudentAnnouncement>, GetListResponse<GetListByAnnouncementIdStudentAnnouncementResponse>>().ReverseMap();
        CreateMap<IPaginate<StudentAnnouncement>, GetListResponse<GetListForLoggedStudentAnnouncementResponse>>().ReverseMap();

        CreateMap<StudentAnnouncement, GetListStudentAnnouncementListItemDto>()
          .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.User.FirstName))
          .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.User.LastName))
          .ForMember(dest => dest.StudentEmail, opt => opt.MapFrom(src => src.Student.User.Email))
          ;

        CreateMap<StudentAnnouncement, GetListByAnnouncementIdStudentAnnouncementResponse>()
          .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.User.FirstName))
          .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.User.LastName))
          .ForMember(dest => dest.StudentEmail, opt => opt.MapFrom(src => src.Student.User.Email))
          ;
    }
}