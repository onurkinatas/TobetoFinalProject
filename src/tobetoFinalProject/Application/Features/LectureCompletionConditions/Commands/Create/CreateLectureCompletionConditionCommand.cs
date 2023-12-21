using Application.Features.LectureCompletionConditions.Constants;
using Application.Features.LectureCompletionConditions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.LectureCompletionConditions.Constants.LectureCompletionConditionsOperationClaims;

namespace Application.Features.LectureCompletionConditions.Commands.Create;

public class CreateLectureCompletionConditionCommand : IRequest<CreatedLectureCompletionConditionResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public int CompletionPercentage { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureCompletionConditionsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureCompletionConditions";

    public class CreateLectureCompletionConditionCommandHandler : IRequestHandler<CreateLectureCompletionConditionCommand, CreatedLectureCompletionConditionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;
        private readonly LectureCompletionConditionBusinessRules _lectureCompletionConditionBusinessRules;

        public CreateLectureCompletionConditionCommandHandler(IMapper mapper, ILectureCompletionConditionRepository lectureCompletionConditionRepository,
                                         LectureCompletionConditionBusinessRules lectureCompletionConditionBusinessRules)
        {
            _mapper = mapper;
            _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
            _lectureCompletionConditionBusinessRules = lectureCompletionConditionBusinessRules;
        }

        public async Task<CreatedLectureCompletionConditionResponse> Handle(CreateLectureCompletionConditionCommand request, CancellationToken cancellationToken)
        {
            LectureCompletionCondition lectureCompletionCondition = _mapper.Map<LectureCompletionCondition>(request);

            await _lectureCompletionConditionRepository.AddAsync(lectureCompletionCondition);

            CreatedLectureCompletionConditionResponse response = _mapper.Map<CreatedLectureCompletionConditionResponse>(lectureCompletionCondition);
            return response;
        }
    }
}