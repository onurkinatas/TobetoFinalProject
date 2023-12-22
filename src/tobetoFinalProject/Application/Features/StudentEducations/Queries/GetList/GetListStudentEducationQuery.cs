using Application.Features.StudentEducations.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentEducations.Constants.StudentEducationsOperationClaims;
using Application.Services.CacheForMemory;

namespace Application.Features.StudentEducations.Queries.GetList;

public class GetListStudentEducationQuery : IRequest<GetListResponse<GetListStudentEducationListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentEducations({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentEducations";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentEducationQueryHandler : IRequestHandler<GetListStudentEducationQuery, GetListResponse<GetListStudentEducationListItemDto>>
    {
        private readonly IStudentEducationRepository _studentEducationRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetListStudentEducationQueryHandler(IStudentEducationRepository studentEducationRepository, IMapper mapper, ICacheMemoryService cacheMemoryService)
        {
            _studentEducationRepository = studentEducationRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetListResponse<GetListStudentEducationListItemDto>> Handle(GetListStudentEducationQuery request, CancellationToken cancellationToken)
        {
            var cacheMemoryStudentId = _cacheMemoryService.GetStudentIdFromCache();

            IPaginate<StudentEducation> studentEducations = await _studentEducationRepository.GetListAsync(
                predicate: s => s.StudentId == cacheMemoryStudentId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentEducationListItemDto> response = _mapper.Map<GetListResponse<GetListStudentEducationListItemDto>>(studentEducations);
            return response;
        }
    }
}