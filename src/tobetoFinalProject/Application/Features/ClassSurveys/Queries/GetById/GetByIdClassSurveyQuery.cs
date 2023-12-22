using Application.Features.ClassSurveys.Constants;
using Application.Features.ClassSurveys.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ClassSurveys.Constants.ClassSurveysOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.CacheForMemory;

namespace Application.Features.ClassSurveys.Queries.GetById;

public class GetByIdClassSurveyQuery : IRequest<GetByIdClassSurveyResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetByIdClassSurveyQueryHandler : IRequestHandler<GetByIdClassSurveyQuery, GetByIdClassSurveyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassSurveyRepository _classSurveyRepository;
        private readonly ClassSurveyBusinessRules _classSurveyBusinessRules;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetByIdClassSurveyQueryHandler(IMapper mapper, IClassSurveyRepository classSurveyRepository, ClassSurveyBusinessRules classSurveyBusinessRules, ICacheMemoryService cacheMemoryService)
        {
            _mapper = mapper;
            _classSurveyRepository = classSurveyRepository;
            _classSurveyBusinessRules = classSurveyBusinessRules;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetByIdClassSurveyResponse> Handle(GetByIdClassSurveyQuery request, CancellationToken cancellationToken)
        {
            IList<Guid> getCacheClassIds = _cacheMemoryService.GetStudentClassIdFromCache();

            ClassSurvey? classSurvey = await _classSurveyRepository.GetAsync(
                predicate: c => getCacheClassIds.Contains(c.StudentClassId),
                include: c => c.Include(c => c.StudentClass)
                    .Include(c => c.Survey),
                cancellationToken: cancellationToken);
            await _classSurveyBusinessRules.ClassSurveyShouldExistWhenSelected(classSurvey);

            GetByIdClassSurveyResponse response = _mapper.Map<GetByIdClassSurveyResponse>(classSurvey);
            return response;
        }
    }
}