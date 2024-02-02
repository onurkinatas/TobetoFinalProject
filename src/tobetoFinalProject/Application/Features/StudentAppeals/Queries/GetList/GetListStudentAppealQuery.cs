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
using Application.Services.ContextOperations;

namespace Application.Features.StudentAppeals.Queries.GetList;

public class GetListStudentAppealQuery : IRequest<GetListResponse<GetListStudentAppealListItemDto>>, ISecuredRequest
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
        private readonly IContextOperationService _contextOperationService;
        public GetListStudentAppealQueryHandler(
            IStudentAppealRepository studentAppealRepository,
            IMapper mapper,
            ICacheMemoryService cacheMemoryService,
            IStudentStageRepository studentStageRepository,
            IAppealStageRepository appealStageRepository,
            IContextOperationService contextOperationService)
        {
            _studentAppealRepository = studentAppealRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _studentStageRepository = studentStageRepository;
            _appealStageRepository = appealStageRepository;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentAppealListItemDto>> Handle(GetListStudentAppealQuery request, CancellationToken cancellationToken)
        {
            Student getStudentFromContext = await _contextOperationService.GetStudentFromContext();

            IPaginate<StudentStage> studentStages = await _studentStageRepository.GetListAsync(
                predicate: s => s.StudentId == getStudentFromContext.Id,
                cancellationToken: cancellationToken
            ); 
            var studentStageIds = studentStages.Items.Select(s => s.StageId).ToList();

            var studentAppeals = await _studentAppealRepository.GetListAsync(
                    predicate: sa => sa.StudentId == getStudentFromContext.Id,
                    include: sa => sa.Include(sa => sa.Appeal.AppealStages
                    .Where(aps => studentStageIds
                    .Contains(aps.StageId)))
                    .ThenInclude(sas => sas.Stage.StudentStages
                    .Where(ss => ss.StudentId == getStudentFromContext.Id && studentStageIds
                    .Contains(ss.StageId))),
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                    cancellationToken: cancellationToken
                );;

            var response = _mapper.Map<GetListResponse<GetListStudentAppealListItemDto>>(studentAppeals);
            return response;
        }
    }
}