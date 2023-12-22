using Application.Features.StudentAppeals.Constants;
using Application.Features.StudentAppeals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentAppeals.Constants.StudentAppealsOperationClaims;
using Application.Services.CacheForMemory;

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

        public GetByIdStudentAppealQueryHandler(IMapper mapper, IStudentAppealRepository studentAppealRepository, StudentAppealBusinessRules studentAppealBusinessRules, ICacheMemoryService cacheMemoryService)
        {
            _mapper = mapper;
            _studentAppealRepository = studentAppealRepository;
            _studentAppealBusinessRules = studentAppealBusinessRules;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetByIdStudentAppealResponse> Handle(GetByIdStudentAppealQuery request, CancellationToken cancellationToken)
        {
            var cacheMemoryStudentId = _cacheMemoryService.GetStudentIdFromCache();

            StudentAppeal? studentAppeal = await _studentAppealRepository.GetAsync(
                predicate: sa => sa.Id == request.Id && sa.StudentId == cacheMemoryStudentId,
                cancellationToken: cancellationToken);
            await _studentAppealBusinessRules.StudentAppealShouldExistWhenSelected(studentAppeal);

            GetByIdStudentAppealResponse response = _mapper.Map<GetByIdStudentAppealResponse>(studentAppeal);
            return response;
        }
    }
}