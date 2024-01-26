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

namespace Application.Features.LectureViews.Commands.Update;

public class UpdateLectureViewCommand : IRequest<UpdatedLectureViewResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public Guid ContentId { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureViewsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureViews";

    public class UpdateLectureViewCommandHandler : IRequestHandler<UpdateLectureViewCommand, UpdatedLectureViewResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureViewRepository _lectureViewRepository;
        private readonly LectureViewBusinessRules _lectureViewBusinessRules;

        public UpdateLectureViewCommandHandler(IMapper mapper, ILectureViewRepository lectureViewRepository,
                                         LectureViewBusinessRules lectureViewBusinessRules)
        {
            _mapper = mapper;
            _lectureViewRepository = lectureViewRepository;
            _lectureViewBusinessRules = lectureViewBusinessRules;
        }

        public async Task<UpdatedLectureViewResponse> Handle(UpdateLectureViewCommand request, CancellationToken cancellationToken)
        {
            LectureView? lectureView = await _lectureViewRepository.GetAsync(predicate: lv => lv.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureViewBusinessRules.LectureViewShouldExistWhenSelected(lectureView);
            lectureView = _mapper.Map(request, lectureView);

            await _lectureViewRepository.UpdateAsync(lectureView!);

            UpdatedLectureViewResponse response = _mapper.Map<UpdatedLectureViewResponse>(lectureView);
            return response;
        }
    }
}