using Application.Features.ClassSurveys.Constants;
using Application.Features.ClassSurveys.Constants;
using Application.Features.ClassSurveys.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ClassSurveys.Constants.ClassSurveysOperationClaims;

namespace Application.Features.ClassSurveys.Commands.Delete;

public class DeleteClassSurveyCommand : IRequest<DeletedClassSurveyResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassSurveysOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetClassSurveys";

    public class DeleteClassSurveyCommandHandler : IRequestHandler<DeleteClassSurveyCommand, DeletedClassSurveyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassSurveyRepository _classSurveyRepository;
        private readonly ClassSurveyBusinessRules _classSurveyBusinessRules;

        public DeleteClassSurveyCommandHandler(IMapper mapper, IClassSurveyRepository classSurveyRepository,
                                         ClassSurveyBusinessRules classSurveyBusinessRules)
        {
            _mapper = mapper;
            _classSurveyRepository = classSurveyRepository;
            _classSurveyBusinessRules = classSurveyBusinessRules;
        }

        public async Task<DeletedClassSurveyResponse> Handle(DeleteClassSurveyCommand request, CancellationToken cancellationToken)
        {
            ClassSurvey? classSurvey = await _classSurveyRepository.GetAsync(predicate: cs => cs.Id == request.Id, cancellationToken: cancellationToken);
            await _classSurveyBusinessRules.ClassSurveyShouldExistWhenSelected(classSurvey);

            await _classSurveyRepository.DeleteAsync(classSurvey!);

            DeletedClassSurveyResponse response = _mapper.Map<DeletedClassSurveyResponse>(classSurvey);
            return response;
        }
    }
}