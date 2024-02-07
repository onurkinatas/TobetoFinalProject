using Application.Features.LectureCompletionConditions.Commands.Create;
using Application.Features.LectureCompletionConditions.Commands.Delete;
using Application.Features.LectureCompletionConditions.Commands.Update;
using Application.Features.LectureCompletionConditions.Queries.GetById;
using Application.Features.LectureCompletionConditions.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.Students.Queries.GetList;

namespace Application.Features.LectureCompletionConditions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<LectureCompletionCondition, CreateLectureCompletionConditionCommand>().ReverseMap();
        CreateMap<LectureCompletionCondition, CreatedLectureCompletionConditionResponse>().ReverseMap();
        CreateMap<LectureCompletionCondition, UpdateLectureCompletionConditionCommand>().ReverseMap();
        CreateMap<LectureCompletionCondition, UpdatedLectureCompletionConditionResponse>().ReverseMap();
        CreateMap<LectureCompletionCondition, DeleteLectureCompletionConditionCommand>().ReverseMap();
        CreateMap<LectureCompletionCondition, DeletedLectureCompletionConditionResponse>().ReverseMap();
        CreateMap<LectureCompletionCondition, GetByIdLectureCompletionConditionResponse>().ReverseMap();
        CreateMap<LectureCompletionCondition, GetListLectureCompletionConditionListItemDto>().ReverseMap();
        CreateMap<IPaginate<LectureCompletionCondition>, GetListResponse<GetListLectureCompletionConditionListItemDto>>().ReverseMap();

        CreateMap<LectureCompletionCondition, GetListLectureCompletionConditionListItemDto>()
            .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.User.FirstName))
            .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.User.LastName))
            .ForMember(dest => dest.StudentEmail, opt => opt.MapFrom(src => src.Student.User.Email))
            .ForMember(dest => dest.LectureName, opt => opt.MapFrom(src => src.Lecture.Name))
            .ForMember(dest => dest.LectureImageUrl, opt => opt.MapFrom(src => src.Lecture.ImageUrl))
            ;
    }
}