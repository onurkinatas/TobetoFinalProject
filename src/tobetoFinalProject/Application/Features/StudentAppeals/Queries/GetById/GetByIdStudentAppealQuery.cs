using Application.Features.StudentAppeals.Constants;
using Application.Features.StudentAppeals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentAppeals.Constants.StudentAppealsOperationClaims;
using Application.Services.CacheForMemory;
using Microsoft.EntityFrameworkCore;
using Core.Persistence.Paging;

namespace Application.Features.StudentAppeals.Queries.GetById;

public class GetByIdStudentAppealQuery : IRequest<GetByIdStudentAppealResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetByIdStudentAppealQueryHandler : IRequestHandler<GetByIdStudentAppealQuery, GetByIdStudentAppealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentAppealRepository _studentAppealRepository;
        private readonly StudentAppealBusinessRules _studentAppealBusinessRules;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IStudentStageRepository _studentStageRepository;

        public GetByIdStudentAppealQueryHandler(IMapper mapper, IStudentAppealRepository studentAppealRepository, StudentAppealBusinessRules studentAppealBusinessRules, ICacheMemoryService cacheMemoryService, IStudentStageRepository studentStageRepository)
        {
            _mapper = mapper;
            _studentAppealRepository = studentAppealRepository;
            _studentAppealBusinessRules = studentAppealBusinessRules;
            _cacheMemoryService = cacheMemoryService;
            _studentStageRepository = studentStageRepository;
        }

        public async Task<GetByIdStudentAppealResponse> Handle(GetByIdStudentAppealQuery request, CancellationToken cancellationToken)
        {
            var cacheMemoryStudentId = _cacheMemoryService.GetStudentIdFromCache();

            IPaginate<StudentStage> studentStages = await _studentStageRepository.GetListAsync(
                predicate: s => s.StudentId == cacheMemoryStudentId,
                cancellationToken: cancellationToken
            );
            var studentStageIds = studentStages.Items.Select(s => s.StageId).ToList();

            StudentAppeal? studentAppeal = await _studentAppealRepository.GetAsync(
                predicate: sa => sa.Id == request.Id && sa.StudentId == cacheMemoryStudentId,
                include: sa => sa.Include(sa => sa.Appeal.AppealStages
                    .Where(aps => studentStageIds
                    .Contains(aps.StageId)))
                    .ThenInclude(sas => sas.Stage.StudentStages
                    .Where(ss => ss.StudentId == cacheMemoryStudentId && studentStageIds
                    .Contains(ss.StageId))),
                cancellationToken: cancellationToken);
            await _studentAppealBusinessRules.StudentAppealShouldExistWhenSelected(studentAppeal);

            GetByIdStudentAppealResponse response = _mapper.Map<GetByIdStudentAppealResponse>(studentAppeal);
            return response;
        }
    }
}