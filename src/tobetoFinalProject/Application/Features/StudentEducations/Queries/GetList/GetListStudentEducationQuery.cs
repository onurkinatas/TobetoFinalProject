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
using Application.Services.ContextOperations;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudentEducations.Queries.GetList;

public class GetListStudentEducationQuery : IRequest<GetListResponse<GetListStudentEducationListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin};

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentEducations({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentEducations";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentEducationQueryHandler : IRequestHandler<GetListStudentEducationQuery, GetListResponse<GetListStudentEducationListItemDto>>
    {
        private readonly IStudentEducationRepository _studentEducationRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListStudentEducationQueryHandler(IStudentEducationRepository studentEducationRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentEducationRepository = studentEducationRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentEducationListItemDto>> Handle(GetListStudentEducationQuery request, CancellationToken cancellationToken)
        {

            IPaginate<StudentEducation> studentEducations = await _studentEducationRepository.GetListAsync(
                include: s => s.Include(s => s.Student).ThenInclude(s => s.User),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentEducationListItemDto> response = _mapper.Map<GetListResponse<GetListStudentEducationListItemDto>>(studentEducations);
            return response;
        }
    }
}