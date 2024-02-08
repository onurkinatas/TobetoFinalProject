using Application.Features.StudentPrivateCertificates.Commands.Create;
using Application.Features.StudentPrivateCertificates.Commands.Delete;
using Application.Features.StudentPrivateCertificates.Commands.Update;
using Application.Features.StudentPrivateCertificates.Queries.GetById;
using Application.Features.StudentPrivateCertificates.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.StudentPrivateCertificates.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentPrivateCertificate, CreateStudentPrivateCertificateCommand>().ReverseMap();
        CreateMap<StudentPrivateCertificate, CreatedStudentPrivateCertificateResponse>().ReverseMap();
        CreateMap<StudentPrivateCertificate, UpdateStudentPrivateCertificateCommand>().ReverseMap();
        CreateMap<StudentPrivateCertificate, UpdatedStudentPrivateCertificateResponse>().ReverseMap();
        CreateMap<StudentPrivateCertificate, DeleteStudentPrivateCertificateCommand>().ReverseMap();
        CreateMap<StudentPrivateCertificate, DeletedStudentPrivateCertificateResponse>().ReverseMap();
        CreateMap<StudentPrivateCertificate, GetByIdStudentPrivateCertificateResponse>().ReverseMap();
        CreateMap<StudentPrivateCertificate, GetListStudentPrivateCertificateListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentPrivateCertificate>, GetListResponse<GetListStudentPrivateCertificateListItemDto>>().ReverseMap();
    }
}