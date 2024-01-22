using Application.Features.StudentExperiences.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentExperiences.Constants.StudentExperiencesOperationClaims;
using Application.Services.CacheForMemory;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Core.Security.Extensions;
using Application.Services.ContextOperations;

namespace Application.Features.StudentExperiences.Queries.GetList;

public class GetListStudentExperienceQuery : IRequest<GetListResponse<GetListStudentExperienceListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentExperiences({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentExperiences";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentExperienceQueryHandler : IRequestHandler<GetListStudentExperienceQuery, GetListResponse<GetListStudentExperienceListItemDto>>
    {
        private readonly IStudentExperienceRepository _studentExperienceRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStudentRepository _studentRepository;
        private readonly IContextOperationService _contextOperationService;


        public GetListStudentExperienceQueryHandler(IStudentExperienceRepository studentExperienceRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IStudentRepository studentRepository, IContextOperationService contextOperationService)
        {
            _studentExperienceRepository = studentExperienceRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _studentRepository = studentRepository;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentExperienceListItemDto>> Handle(GetListStudentExperienceQuery request, CancellationToken cancellationToken)
        {
            Student student = await _contextOperationService.GetStudentFromContext();
            
            IPaginate<StudentExperience> studentExperiences = await _studentExperienceRepository.GetListAsync(
                predicate: se => se.StudentId == student.Id,
                include: se => se.Include(se => se.City),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentExperienceListItemDto> response = _mapper.Map<GetListResponse<GetListStudentExperienceListItemDto>>(studentExperiences);
            return response;
        }
    }
}