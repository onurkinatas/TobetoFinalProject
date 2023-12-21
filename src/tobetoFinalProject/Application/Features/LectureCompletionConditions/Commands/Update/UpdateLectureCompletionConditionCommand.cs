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

namespace Application.Features.LectureCompletionConditions.Commands.Update;

public class UpdateLectureCompletionConditionCommand : IRequest<UpdatedLectureCompletionConditionResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public int CompletionPercentage { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureCompletionConditionsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureCompletionConditions";

    public class UpdateLectureCompletionConditionCommandHandler : IRequestHandler<UpdateLectureCompletionConditionCommand, UpdatedLectureCompletionConditionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;
        private readonly LectureCompletionConditionBusinessRules _lectureCompletionConditionBusinessRules;

        public UpdateLectureCompletionConditionCommandHandler(IMapper mapper, ILectureCompletionConditionRepository lectureCompletionConditionRepository,
                                         LectureCompletionConditionBusinessRules lectureCompletionConditionBusinessRules)
        {
            _mapper = mapper;
            _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
            _lectureCompletionConditionBusinessRules = lectureCompletionConditionBusinessRules;
        }

        public async Task<UpdatedLectureCompletionConditionResponse> Handle(UpdateLectureCompletionConditionCommand request, CancellationToken cancellationToken)
        {
            LectureCompletionCondition? lectureCompletionCondition = await _lectureCompletionConditionRepository.GetAsync(predicate: lcc => lcc.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureCompletionConditionBusinessRules.LectureCompletionConditionShouldExistWhenSelected(lectureCompletionCondition);
            lectureCompletionCondition = _mapper.Map(request, lectureCompletionCondition);

            await _lectureCompletionConditionRepository.UpdateAsync(lectureCompletionCondition!);

            UpdatedLectureCompletionConditionResponse response = _mapper.Map<UpdatedLectureCompletionConditionResponse>(lectureCompletionCondition);
            return response;
        }
    }
}