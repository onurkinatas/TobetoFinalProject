using Application.Features.StudentAppeals.Commands.Create;
using Application.Features.StudentAppeals.Commands.Delete;
using Application.Features.StudentAppeals.Commands.Update;
using Application.Features.StudentAppeals.Queries.GetById;
using Application.Features.StudentAppeals.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.StudentAppeals.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StudentAppeal, CreateStudentAppealCommand>().ReverseMap();
        CreateMap<StudentAppeal, CreatedStudentAppealResponse>().ReverseMap();
        CreateMap<StudentAppeal, UpdateStudentAppealCommand>().ReverseMap();
        CreateMap<StudentAppeal, UpdatedStudentAppealResponse>().ReverseMap();
        CreateMap<StudentAppeal, DeleteStudentAppealCommand>().ReverseMap();
        CreateMap<StudentAppeal, DeletedStudentAppealResponse>().ReverseMap();
        CreateMap<StudentAppeal, GetByIdStudentAppealResponse>().ReverseMap();
        CreateMap<StudentAppeal, GetListStudentAppealListItemDto>().ReverseMap();
        CreateMap<IPaginate<StudentAppeal>, GetListResponse<GetListStudentAppealListItemDto>>().ReverseMap();

        CreateMap<StudentAppeal, GetListStudentAppealListItemDto>()
            .ForPath(dest => dest.Stages, opt => opt.MapFrom(src => src.Appeal.AppealStages
            .Select(sa => sa.Stage).ToList()));

    }
}