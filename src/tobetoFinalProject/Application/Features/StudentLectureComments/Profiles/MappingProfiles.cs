using Application.Features.StudentLectureComments.Commands.Create;
using Application.Features.StudentLectureComments.Commands.Delete;
using Application.Features.StudentLectureComments.Commands.Update;
using Application.Features.StudentLectureComments.Queries.GetById;
using Application.Features.StudentLectureComments.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

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
    }
}