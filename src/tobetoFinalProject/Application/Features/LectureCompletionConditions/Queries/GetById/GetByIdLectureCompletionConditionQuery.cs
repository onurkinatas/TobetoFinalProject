using Application.Features.LectureCompletionConditions.Constants;
using Application.Features.LectureCompletionConditions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.LectureCompletionConditions.Constants.LectureCompletionConditionsOperationClaims;
using Application.Services.ContextOperations;

namespace Application.Features.LectureCompletionConditions.Queries.GetById;

public class GetByIdLectureCompletionConditionQuery : IRequest<GetByIdLectureCompletionConditionResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdLectureCompletionConditionQueryHandler : IRequestHandler<GetByIdLectureCompletionConditionQuery, GetByIdLectureCompletionConditionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;
        private readonly LectureCompletionConditionBusinessRules _lectureCompletionConditionBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        public GetByIdLectureCompletionConditionQueryHandler(IMapper mapper, ILectureCompletionConditionRepository lectureCompletionConditionRepository, LectureCompletionConditionBusinessRules lectureCompletionConditionBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
            _lectureCompletionConditionBusinessRules = lectureCompletionConditionBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetByIdLectureCompletionConditionResponse> Handle(GetByIdLectureCompletionConditionQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            LectureCompletionCondition? lectureCompletionCondition = await _lectureCompletionConditionRepository.GetAsync(predicate: lcc => lcc.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureCompletionConditionBusinessRules.LectureCompletionConditionShouldExistWhenSelected(lectureCompletionCondition);

            GetByIdLectureCompletionConditionResponse response = _mapper.Map<GetByIdLectureCompletionConditionResponse>(lectureCompletionCondition);
            return response;
        }
    }
}