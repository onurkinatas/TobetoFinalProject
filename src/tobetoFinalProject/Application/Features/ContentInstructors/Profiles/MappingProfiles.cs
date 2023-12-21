using Application.Features.ContentInstructors.Commands.Create;
using Application.Features.ContentInstructors.Commands.Delete;
using Application.Features.ContentInstructors.Commands.Update;
using Application.Features.ContentInstructors.Queries.GetById;
using Application.Features.ContentInstructors.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContentInstructors.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentInstructor, CreateContentInstructorCommand>().ReverseMap();
        CreateMap<ContentInstructor, CreatedContentInstructorResponse>().ReverseMap();
        CreateMap<ContentInstructor, UpdateContentInstructorCommand>().ReverseMap();
        CreateMap<ContentInstructor, UpdatedContentInstructorResponse>().ReverseMap();
        CreateMap<ContentInstructor, DeleteContentInstructorCommand>().ReverseMap();
        CreateMap<ContentInstructor, DeletedContentInstructorResponse>().ReverseMap();
        CreateMap<ContentInstructor, GetByIdContentInstructorResponse>().ReverseMap();
        CreateMap<ContentInstructor, GetListContentInstructorListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentInstructor>, GetListResponse<GetListContentInstructorListItemDto>>().ReverseMap();
    }
}