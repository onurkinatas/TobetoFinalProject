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

namespace Application.Features.ClassSurveys.Commands.Update;

public class UpdateClassSurveyCommand : IRequest<UpdatedClassSurveyResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid StudentClassId { get; set; }
    public Guid SurveyId { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassSurveysOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllClassDetails";

    public class UpdateClassSurveyCommandHandler : IRequestHandler<UpdateClassSurveyCommand, UpdatedClassSurveyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassSurveyRepository _classSurveyRepository;
        private readonly ClassSurveyBusinessRules _classSurveyBusinessRules;

        public UpdateClassSurveyCommandHandler(IMapper mapper, IClassSurveyRepository classSurveyRepository,
                                         ClassSurveyBusinessRules classSurveyBusinessRules)
        {
            _mapper = mapper;
            _classSurveyRepository = classSurveyRepository;
            _classSurveyBusinessRules = classSurveyBusinessRules;
        }

        public async Task<UpdatedClassSurveyResponse> Handle(UpdateClassSurveyCommand request, CancellationToken cancellationToken)
        {
            ClassSurvey? classSurvey = await _classSurveyRepository.GetAsync(predicate: cs => cs.Id == request.Id, cancellationToken: cancellationToken);
            await _classSurveyBusinessRules.ClassSurveyShouldExistWhenSelected(classSurvey);
            classSurvey = _mapper.Map(request, classSurvey);
            await _classSurveyBusinessRules.ClassSurveyShouldNotExistsWhenInsert(classSurvey.StudentClassId, classSurvey.SurveyId);
            await _classSurveyRepository.UpdateAsync(classSurvey!);

            UpdatedClassSurveyResponse response = _mapper.Map<UpdatedClassSurveyResponse>(classSurvey);
            return response;
        }
    }
}