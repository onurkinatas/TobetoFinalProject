using Application.Features.StudentCertificates.Commands.Create;
using Application.Features.StudentCertificates.Commands.Delete;
using Application.Features.StudentCertificates.Commands.Update;
using Application.Features.StudentCertificates.Queries.GetById;
using Application.Features.StudentCertificates.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.StudentCertificates.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentCertificate, CreateStudentCertificateCommand>().ReverseMap();
        CreateMap<StudentCertificate, CreatedStudentCertificateResponse>().ReverseMap();
        CreateMap<StudentCertificate, UpdateStudentCertificateCommand>().ReverseMap();
        CreateMap<StudentCertificate, UpdatedStudentCertificateResponse>().ReverseMap();
        CreateMap<StudentCertificate, DeleteStudentCertificateCommand>().ReverseMap();
        CreateMap<StudentCertificate, DeletedStudentCertificateResponse>().ReverseMap();
        CreateMap<StudentCertificate, GetByIdStudentCertificateResponse>().ReverseMap();
        CreateMap<StudentCertificate, GetListStudentCertificateListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentCertificate>, GetListResponse<GetListStudentCertificateListItemDto>>().ReverseMap();
    }
}