using Application.Features.StudentExams.Commands.Create;
using Application.Features.StudentExams.Commands.Delete;
using Application.Features.StudentExams.Commands.Update;
using Application.Features.StudentExams.Queries.GetById;
using Application.Features.StudentExams.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.StudentExams.Queries.GetListForLoggedStudent;

namespace Application.Features.StudentExams.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentExam, CreateStudentExamCommand>().ReverseMap();
        CreateMap<StudentExam, CreatedStudentExamResponse>().ReverseMap();
        CreateMap<StudentExam, UpdateStudentExamCommand>().ReverseMap();
        CreateMap<StudentExam, UpdatedStudentExamResponse>().ReverseMap();
        CreateMap<StudentExam, DeleteStudentExamCommand>().ReverseMap();
        CreateMap<StudentExam, DeletedStudentExamResponse>().ReverseMap();
        CreateMap<StudentExam, GetByIdStudentExamResponse>().ReverseMap();
        CreateMap<StudentExam, GetListStudentExamListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentExam>, GetListResponse<GetListStudentExamListItemDto>>().ReverseMap();

        CreateMap<StudentExam, GetListStudentExamForLoggedStudentListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentExam>, GetListResponse<GetListStudentExamForLoggedStudentListItemDto>>().ReverseMap();
    }
}