using Application.Features.StudentEducations.Commands.Create;
using Application.Features.StudentEducations.Commands.Delete;
using Application.Features.StudentEducations.Commands.Update;
using Application.Features.StudentEducations.Queries.GetById;
using Application.Features.StudentEducations.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.StudentEducations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentEducation, CreateStudentEducationCommand>().ReverseMap();
        CreateMap<StudentEducation, CreatedStudentEducationResponse>().ReverseMap();
        CreateMap<StudentEducation, UpdateStudentEducationCommand>().ReverseMap();
        CreateMap<StudentEducation, UpdatedStudentEducationResponse>().ReverseMap();
        CreateMap<StudentEducation, DeleteStudentEducationCommand>().ReverseMap();
        CreateMap<StudentEducation, DeletedStudentEducationResponse>().ReverseMap();
        CreateMap<StudentEducation, GetByIdStudentEducationResponse>().ReverseMap();
        CreateMap<StudentEducation, GetListStudentEducationListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentEducation>, GetListResponse<GetListStudentEducationListItemDto>>().ReverseMap();
    }
}