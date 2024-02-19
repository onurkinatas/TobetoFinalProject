using Application.Features.LectureViews.Constants;
using Application.Features.LectureViews.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.LectureViews.Constants.LectureViewsOperationClaims;
using Application.Services.ContextOperations;
using Microsoft.EntityFrameworkCore;
using Application.Services.Lectures;
using Application.Services.LectureViews;
using Application.Services.LectureCompletionConditions;

namespace Application.Features.LectureViews.Commands.Create;

public class CreateLectureViewCommand : IRequest<CreatedLectureViewResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid? StudentId { get; set; }
    public Guid LectureId { get; set; }
    public Guid ContentId { get; set; }
    public int? UserId { get; set; }
    public string[] Roles => new[] { Admin, Write, LectureViewsOperationClaims.Create ,"Student"};

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => $"GetLectureCompletionConditionsForCompletedAndContinued({UserId})";

    public class CreateLectureViewCommandHandler : IRequestHandler<CreateLectureViewCommand, CreatedLectureViewResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureViewRepository _lectureViewRepository;
        private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;
        private readonly LectureViewBusinessRules _lectureViewBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        private readonly ILectureRepository _lectureRepository;
        private readonly ILecturesService _lecturesService;
        private readonly ILectureViewsService _lectureViewsService;
        private readonly ILectureCompletionConditionsService _lectureCompletionConditionsService;
        public CreateLectureViewCommandHandler(IMapper mapper, ILectureViewRepository lectureViewRepository,
                                         LectureViewBusinessRules lectureViewBusinessRules, IContextOperationService contextOperationService,
                                         ILectureRepository lectureRepository, ILectureCompletionConditionRepository lectureCompletionConditionRepository
                                         , ILecturesService lecturesService, ILectureViewsService lectureViewsService, ILectureCompletionConditionsService lectureCompletionConditionsService)
        {
            _lecturesService = lecturesService;
            _mapper = mapper;
            _lectureViewRepository = lectureViewRepository;
            _lectureViewBusinessRules = lectureViewBusinessRules;
            _contextOperationService = contextOperationService;
            _lectureRepository = lectureRepository;
            _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
            _lectureViewsService = lectureViewsService;
            _lectureCompletionConditionsService = lectureCompletionConditionsService;
        }

        public async Task<CreatedLectureViewResponse> Handle(CreateLectureViewCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            LectureView lectureView = _mapper.Map<LectureView>(request);
            lectureView.StudentId = getStudent.Id;

            LectureView? existLectureView = await _lectureViewRepository.GetAsync(predicate: lw => lw.StudentId == getStudent.Id && lw.ContentId == lectureView.ContentId &&
                                                                                         lw.LectureId==lectureView.LectureId ,cancellationToken: cancellationToken);
            
            if (existLectureView is null)
            {
                await _lectureViewRepository.AddAsync(lectureView);

                LectureCompletionCondition? doesExistLectureCompletionCondition = await _lectureCompletionConditionRepository.GetAsync(predicate: lcc => lcc.LectureId == lectureView.LectureId 
                                                                                                                                                             && lcc.StudentId == getStudent.Id);

                int contentCount = await _lecturesService.GetAllContentCountByLectureId(request.LectureId,cancellationToken);
                int lectureViewCount = await _lectureViewsService.ContentViewedByLectureId(request.LectureId, getStudent.Id);
                int completionPercentage = await _lectureCompletionConditionsService.CompletionPercentageCalculator(lectureViewCount, contentCount);

                if (doesExistLectureCompletionCondition is null)
                {
                    await _lectureCompletionConditionsService.AddAsync(new LectureCompletionCondition {StudentId=getStudent.Id,LectureId=lectureView.LectureId,CompletionPercentage=completionPercentage });
                }
                    
                else if(doesExistLectureCompletionCondition is not null)
                {
                    doesExistLectureCompletionCondition.CompletionPercentage = completionPercentage;
                    await _lectureCompletionConditionsService.UpdateAsync(doesExistLectureCompletionCondition);
                }
                    

            }
                
            
          

            CreatedLectureViewResponse response = _mapper.Map<CreatedLectureViewResponse>(lectureView);
            return response;
        }
    }
}