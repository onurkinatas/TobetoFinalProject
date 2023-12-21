using Application.Features.StudentClassStudents.Commands.Create;
using Application.Features.StudentClassStudents.Commands.Delete;
using Application.Features.StudentClassStudents.Commands.Update;
using Application.Features.StudentClassStudents.Queries.GetById;
using Application.Features.StudentClassStudents.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.StudentClassStudents.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentClassStudent, CreateStudentClassStudentCommand>().ReverseMap();
        CreateMap<StudentClassStudent, CreatedStudentClassStudentResponse>().ReverseMap();
        CreateMap<StudentClassStudent, UpdateStudentClassStudentCommand>().ReverseMap();
        CreateMap<StudentClassStudent, UpdatedStudentClassStudentResponse>().ReverseMap();
        CreateMap<StudentClassStudent, DeleteStudentClassStudentCommand>().ReverseMap();
        CreateMap<StudentClassStudent, DeletedStudentClassStudentResponse>().ReverseMap();
        CreateMap<StudentClassStudent, GetByIdStudentClassStudentResponse>().ReverseMap();
        CreateMap<StudentClassStudent, GetListStudentClassStudentListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentClassStudent>, GetListResponse<GetListStudentClassStudentListItemDto>>().ReverseMap();
    }
}