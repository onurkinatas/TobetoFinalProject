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

namespace Application.Features.ClassSurveys.Commands.Create;

public class CreateClassSurveyCommand : IRequest<CreatedClassSurveyResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid StudentClassId { get; set; }
    public Guid SurveyId { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassSurveysOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllClassDetails";

    public class CreateClassSurveyCommandHandler : IRequestHandler<CreateClassSurveyCommand, CreatedClassSurveyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassSurveyRepository _classSurveyRepository;
        private readonly ClassSurveyBusinessRules _classSurveyBusinessRules;

        public CreateClassSurveyCommandHandler(IMapper mapper, IClassSurveyRepository classSurveyRepository,
                                         ClassSurveyBusinessRules classSurveyBusinessRules)
        {
            _mapper = mapper;
            _classSurveyRepository = classSurveyRepository;
            _classSurveyBusinessRules = classSurveyBusinessRules;
        }

        public async Task<CreatedClassSurveyResponse> Handle(CreateClassSurveyCommand request, CancellationToken cancellationToken)
        {
            ClassSurvey classSurvey = _mapper.Map<ClassSurvey>(request);
            await _classSurveyBusinessRules.ClassSurveyShouldNotExistsWhenInsert(classSurvey.StudentClassId,classSurvey.SurveyId);
            await _classSurveyRepository.AddAsync(classSurvey);

            CreatedClassSurveyResponse response = _mapper.Map<CreatedClassSurveyResponse>(classSurvey);
            return response;
        }
    }
}