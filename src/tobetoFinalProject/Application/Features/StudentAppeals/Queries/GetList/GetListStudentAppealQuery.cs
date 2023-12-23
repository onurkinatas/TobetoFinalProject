using Application.Features.StudentAppeals.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentAppeals.Constants.StudentAppealsOperationClaims;
using Application.Services.CacheForMemory;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudentAppeals.Queries.GetList;

public class GetListStudentAppealQuery : IRequest<GetListResponse<GetListStudentAppealListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentAppeals({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentAppeals";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentAppealQueryHandler : IRequestHandler<GetListStudentAppealQuery, GetListResponse<GetListStudentAppealListItemDto>>
    {
        private readonly IStudentAppealRepository _studentAppealRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IStudentStageRepository _studentStageRepository;
        private readonly IAppealStageRepository _appealStageRepository;

        public GetListStudentAppealQueryHandler(
            IStudentAppealRepository studentAppealRepository,
            IMapper mapper,
            ICacheMemoryService cacheMemoryService,
            IStudentStageRepository studentStageRepository,
            IAppealStageRepository appealStageRepository)
        {
            _studentAppealRepository = studentAppealRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _studentStageRepository = studentStageRepository;
            _appealStageRepository = appealStageRepository;
        }

        public async Task<GetListResponse<GetListStudentAppealListItemDto>> Handle(GetListStudentAppealQuery request, CancellationToken cancellationToken)
        {
            var cacheMemoryStudentId = _cacheMemoryService.GetStudentIdFromCache();

            // Öðrencinin sahip olduðu tüm stage'leri al
            IPaginate<StudentStage> studentStages = await _studentStageRepository.GetListAsync(
                predicate: s => s.StudentId == cacheMemoryStudentId,
                cancellationToken: cancellationToken
            );
            var studentStageIds = studentStages.Items.Select(s => s.StageId).ToList();

            // Bu stage'lerin baðlý olduðu tüm appeal'leri al
            IPaginate<AppealStage> appealStages = await _appealStageRepository.GetListAsync(
                predicate: s => studentStageIds.Contains(s.StageId),
                cancellationToken: cancellationToken
            );
            var appealIds = appealStages.Items.Select(s => s.AppealId).ToList();

            // Sadece öðrencinin sahip olduðu appeal'leri ve bu appeal'e ait sadece öðrencinin sahip olduðu stage'leri al
            var studentAppeals = await _studentAppealRepository
                .GetListAsync(sa => sa.StudentId == cacheMemoryStudentId && appealIds.Contains(sa.AppealId),
                    include: sa => sa.Include(sa => sa.Appeal.AppealStages.Where(aps => studentStageIds.Contains(aps.StageId)))
    .ThenInclude(sas => sas.Stage.StudentStages.Where(ss => ss.StudentId == cacheMemoryStudentId && studentStageIds.Contains(ss.StageId))),
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                );

            // Haritalamayý gerçekleþtir
            var response = _mapper.Map<GetListResponse<GetListStudentAppealListItemDto>>(studentAppeals);
            return response;
        }
    }
}