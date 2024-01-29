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
        private readonly LectureViewBusinessRules _lectureViewBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        public CreateLectureViewCommandHandler(IMapper mapper, ILectureViewRepository lectureViewRepository,
                                         LectureViewBusinessRules lectureViewBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _lectureViewRepository = lectureViewRepository;
            _lectureViewBusinessRules = lectureViewBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<CreatedLectureViewResponse> Handle(CreateLectureViewCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            LectureView lectureView = _mapper.Map<LectureView>(request);
            lectureView.StudentId = getStudent.Id;

            LectureView? existLectureView = await _lectureViewRepository.GetAsync(predicate: lw => lw.StudentId == getStudent.Id && lw.ContentId == lectureView.ContentId &&lw.LectureId==lectureView.LectureId ,cancellationToken: cancellationToken);
            
            if (existLectureView is null)
                await _lectureViewRepository.AddAsync(lectureView);
            
          

            CreatedLectureViewResponse response = _mapper.Map<CreatedLectureViewResponse>(lectureView);
            return response;
        }
    }
}