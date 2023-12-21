using Application.Features.ClassSurveys.Constants;
using Application.Features.ClassSurveys.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ClassSurveys.Constants.ClassSurveysOperationClaims;

namespace Application.Features.ClassSurveys.Queries.GetById;

public class GetByIdClassSurveyQuery : IRequest<GetByIdClassSurveyResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdClassSurveyQueryHandler : IRequestHandler<GetByIdClassSurveyQuery, GetByIdClassSurveyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassSurveyRepository _classSurveyRepository;
        private readonly ClassSurveyBusinessRules _classSurveyBusinessRules;

        public GetByIdClassSurveyQueryHandler(IMapper mapper, IClassSurveyRepository classSurveyRepository, ClassSurveyBusinessRules classSurveyBusinessRules)
        {
            _mapper = mapper;
            _classSurveyRepository = classSurveyRepository;
            _classSurveyBusinessRules = classSurveyBusinessRules;
        }

        public async Task<GetByIdClassSurveyResponse> Handle(GetByIdClassSurveyQuery request, CancellationToken cancellationToken)
        {
            ClassSurvey? classSurvey = await _classSurveyRepository.GetAsync(predicate: cs => cs.Id == request.Id, cancellationToken: cancellationToken);
            await _classSurveyBusinessRules.ClassSurveyShouldExistWhenSelected(classSurvey);

            GetByIdClassSurveyResponse response = _mapper.Map<GetByIdClassSurveyResponse>(classSurvey);
            return response;
        }
    }
}