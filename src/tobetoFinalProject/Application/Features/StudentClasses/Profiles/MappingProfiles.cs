using Application.Features.StudentClasses.Commands.Create;
using Application.Features.StudentClasses.Commands.Delete;
using Application.Features.StudentClasses.Commands.Update;
using Application.Features.StudentClasses.Queries.GetById;
using Application.Features.StudentClasses.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.StudentClasses.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentClass, CreateStudentClassCommand>().ReverseMap();
        CreateMap<StudentClass, CreatedStudentClassResponse>().ReverseMap();
        CreateMap<StudentClass, UpdateStudentClassCommand>().ReverseMap();
        CreateMap<StudentClass, UpdatedStudentClassResponse>().ReverseMap();
        CreateMap<StudentClass, DeleteStudentClassCommand>().ReverseMap();
        CreateMap<StudentClass, DeletedStudentClassResponse>().ReverseMap();
        CreateMap<StudentClass, GetByIdStudentClassResponse>().ReverseMap();
        CreateMap<StudentClass, GetListStudentClasses>().ReverseMap();
        CreateMap<IPaginate<StudentClass>, GetListResponse<GetListStudentClasses>>().ReverseMap();


        CreateMap<StudentClass, GetListStudentClasses>()
            .ForMember(dest => dest.Exams, opt => opt.MapFrom(src => src.ClassExams.Select(si => si.Exam).ToList()))
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.StudentClassStudentes.Select(si => si.Student).ToList()))
            .ForMember(dest => dest.Announcements, opt => opt.MapFrom(src => src.ClassAnnouncements.Select(si => si.Announcement).ToList()))
            .ForMember(dest => dest.Lectures, opt => opt.MapFrom(src => src.ClassLectures.Select(si => si.Lecture).ToList()))
            .ForMember(dest => dest.Surveys, opt => opt.MapFrom(src => src.ClassSurveys.Select(si => si.Survey).ToList()));


        CreateMap<StudentClass, GetByIdStudentClassResponse>()
            .ForMember(dest => dest.Exams, opt => opt.MapFrom(src => src.ClassExams.Select(si => si.Exam).ToList()))
            .ForMember(dest => dest.Announcements, opt => opt.MapFrom(src => src.ClassAnnouncements.Select(si => si.Announcement).ToList()))
            .ForMember(dest => dest.Lectures, opt => opt.MapFrom(src => src.ClassLectures.Select(si => si.Lecture).ToList()))
            .ForMember(dest => dest.Surveys, opt => opt.MapFrom(src => src.ClassSurveys.Select(si => si.Survey).ToList()));
        ;
    }
}