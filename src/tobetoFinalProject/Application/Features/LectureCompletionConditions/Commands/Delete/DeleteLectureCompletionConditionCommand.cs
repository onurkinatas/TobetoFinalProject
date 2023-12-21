using Application.Features.LectureCompletionConditions.Constants;
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

namespace Application.Features.LectureCompletionConditions.Commands.Delete;

public class DeleteLectureCompletionConditionCommand : IRequest<DeletedLectureCompletionConditionResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureCompletionConditionsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureCompletionConditions";

    public class DeleteLectureCompletionConditionCommandHandler : IRequestHandler<DeleteLectureCompletionConditionCommand, DeletedLectureCompletionConditionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;
        private readonly LectureCompletionConditionBusinessRules _lectureCompletionConditionBusinessRules;

        public DeleteLectureCompletionConditionCommandHandler(IMapper mapper, ILectureCompletionConditionRepository lectureCompletionConditionRepository,
                                         LectureCompletionConditionBusinessRules lectureCompletionConditionBusinessRules)
        {
            _mapper = mapper;
            _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
            _lectureCompletionConditionBusinessRules = lectureCompletionConditionBusinessRules;
        }

        public async Task<DeletedLectureCompletionConditionResponse> Handle(DeleteLectureCompletionConditionCommand request, CancellationToken cancellationToken)
        {
            LectureCompletionCondition? lectureCompletionCondition = await _lectureCompletionConditionRepository.GetAsync(predicate: lcc => lcc.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureCompletionConditionBusinessRules.LectureCompletionConditionShouldExistWhenSelected(lectureCompletionCondition);

            await _lectureCompletionConditionRepository.DeleteAsync(lectureCompletionCondition!);

            DeletedLectureCompletionConditionResponse response = _mapper.Map<DeletedLectureCompletionConditionResponse>(lectureCompletionCondition);
            return response;
        }
    }
}