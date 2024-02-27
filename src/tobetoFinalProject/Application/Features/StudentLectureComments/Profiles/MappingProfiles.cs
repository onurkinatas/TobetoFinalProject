using Application.Features.StudentLectureComments.Commands.Create;
using Application.Features.StudentLectureComments.Commands.Delete;
using Application.Features.StudentLectureComments.Commands.Update;
using Application.Features.StudentLectureComments.Queries.GetById;
using Application.Features.StudentLectureComments.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.StudentLectureComments.Queries.GetListByLectureId;
using Application.Features.Students.Queries.GetList;
using Application.Features.StudentLectureComments.Queries.Dtos;

namespace Application.Features.StudentLectureComments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentLectureComment, CreateStudentLectureCommentCommand>().ReverseMap();
        CreateMap<StudentLectureComment, CreatedStudentLectureCommentResponse>().ReverseMap();
        CreateMap<StudentLectureComment, UpdateStudentLectureCommentCommand>().ReverseMap();
        CreateMap<StudentLectureComment, UpdatedStudentLectureCommentResponse>().ReverseMap();
        CreateMap<StudentLectureComment, DeleteStudentLectureCommentCommand>().ReverseMap();
        CreateMap<StudentLectureComment, DeletedStudentLectureCommentResponse>().ReverseMap();
        CreateMap<StudentLectureComment, GetByIdStudentLectureCommentResponse>().ReverseMap();
        CreateMap<StudentLectureComment, GetListStudentLectureCommentListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentLectureComment>, GetListResponse<GetListStudentLectureCommentListItemDto>>().ReverseMap();
        CreateMap<StudentLectureComment, GetListByLectureIdStudentLectureCommentListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentLectureComment>, GetListResponse<GetListByLectureIdStudentLectureCommentListItemDto>>().ReverseMap();

        
        CreateMap<StudentLectureComment, GetListByLectureIdStudentLectureCommentListItemDto>()
            .ForMember(dest => dest.ProfilePhotoPath, opt => opt.MapFrom(src => src.Student.ProfilePhotoPath))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Student.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Student.User.LastName))
            ;

        CreateMap<CommentSubComment, StudentSubCommentDto>()
            .ForMember(dest => dest.ProfilePhotoPath, opt => opt.MapFrom(src => src.Student.ProfilePhotoPath))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Student.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Student.User.LastName))

             .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.SubComment))
            ;
    }
}