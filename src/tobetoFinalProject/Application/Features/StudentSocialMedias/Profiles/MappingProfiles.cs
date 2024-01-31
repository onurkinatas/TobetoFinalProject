using Application.Features.StudentSocialMedias.Commands.Create;
using Application.Features.StudentSocialMedias.Commands.Delete;
using Application.Features.StudentSocialMedias.Commands.Update;
using Application.Features.StudentSocialMedias.Queries.GetById;
using Application.Features.StudentSocialMedias.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.StudentSkills.Queries.GetList;

namespace Application.Features.StudentSocialMedias.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentSocialMedia, CreateStudentSocialMediaCommand>().ReverseMap();
        CreateMap<StudentSocialMedia, CreatedStudentSocialMediaResponse>().ReverseMap();
        CreateMap<StudentSocialMedia, UpdateStudentSocialMediaCommand>().ReverseMap();
        CreateMap<StudentSocialMedia, UpdatedStudentSocialMediaResponse>().ReverseMap();
        CreateMap<StudentSocialMedia, DeleteStudentSocialMediaCommand>().ReverseMap();
        CreateMap<StudentSocialMedia, DeletedStudentSocialMediaResponse>().ReverseMap();
        CreateMap<StudentSocialMedia, GetByIdStudentSocialMediaResponse>().ReverseMap();
        CreateMap<StudentSocialMedia, GetListStudentSocialMediaListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentSocialMedia>, GetListResponse<GetListStudentSocialMediaListItemDto>>().ReverseMap();

        CreateMap<StudentSocialMedia, GetListStudentSocialMediaListItemDto>()
           .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.User.FirstName))
           .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.User.LastName))
           .ForMember(dest => dest.StudentEmail, opt => opt.MapFrom(src => src.Student.User.Email));
    }
}