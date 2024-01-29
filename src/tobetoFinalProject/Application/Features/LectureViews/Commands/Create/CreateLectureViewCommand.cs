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

namespace Application.Features.LectureViews.Commands.Create;

public class CreateLectureViewCommand : IRequest<CreatedLectureViewResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid? StudentId { get; set; }
    public Guid LectureId { get; set; }
    public Guid ContentId { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureViewsOperationClaims.Create ,"Student"};

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureViews";

    public class CreateLectureViewCommandHandler : IRequestHandler<CreateLectureViewCommand, CreatedLectureViewResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureViewRepository _lectureViewRepository;
        private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;
        private readonly LectureViewBusinessRules _lectureViewBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        private readonly ILectureRepository _lectureRepository;
        public CreateLectureViewCommandHandler(IMapper mapper, ILectureViewRepository lectureViewRepository,
                                         LectureViewBusinessRules lectureViewBusinessRules, IContextOperationService contextOperationService, ILectureRepository lectureRepository, ILectureCompletionConditionRepository lectureCompletionConditionRepository)
        {
            _mapper = mapper;
            _lectureViewRepository = lectureViewRepository;
            _lectureViewBusinessRules = lectureViewBusinessRules;
            _contextOperationService = contextOperationService;
            _lectureRepository = lectureRepository;
            _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
        }

        public async Task<CreatedLectureViewResponse> Handle(CreateLectureViewCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            LectureView lectureView = _mapper.Map<LectureView>(request);
            lectureView.StudentId = getStudent.Id;

            LectureView? existLectureView = await _lectureViewRepository.GetAsync(predicate: lw => lw.StudentId == getStudent.Id && lw.ContentId == lectureView.ContentId &&lw.LectureId==lectureView.LectureId ,cancellationToken: cancellationToken);
            
            if (existLectureView is null)
            {
                await _lectureViewRepository.AddAsync(lectureView);

                LectureCompletionCondition? doesExistLectureCompletionCondition = await _lectureCompletionConditionRepository.GetAsync(predicate: lcc => lcc.LectureId == lectureView.LectureId && lcc.StudentId == getStudent.Id);

                Lecture lecture = await _lectureRepository.GetAsync(
                predicate: l => l.Id == request.LectureId,
                include: l => l.Include(l => l.LectureCourses)
                   .ThenInclude(lc => lc.Course)
                   .ThenInclude(c => c.CourseContents)
                   .ThenInclude(cc => cc.Content),
                cancellationToken: cancellationToken);

                var contentCount = lecture.LectureCourses
                    .SelectMany(lc => lc.Course.CourseContents)
                    .Count();

                ICollection<LectureView> lectureViews = await _lectureViewRepository.GetAll(lv => lv.LectureId == request.LectureId && lv.StudentId == getStudent.Id);
                int lectureViewCount = lectureViews.Count;
                int completionPercentage = (lectureViewCount * 100) / contentCount;

                

                if (doesExistLectureCompletionCondition is null)
                {
                    await _lectureCompletionConditionRepository.AddAsync(new LectureCompletionCondition {StudentId=getStudent.Id,LectureId=lectureView.LectureId,CompletionPercentage=completionPercentage });
                }
                    
                else if(doesExistLectureCompletionCondition is not null)
                {
                    doesExistLectureCompletionCondition.CompletionPercentage = completionPercentage;
                    await _lectureCompletionConditionRepository.UpdateAsync(doesExistLectureCompletionCondition);
                }
                    

            }
                
            
          

            CreatedLectureViewResponse response = _mapper.Map<CreatedLectureViewResponse>(lectureView);
            return response;
        }
    }
}