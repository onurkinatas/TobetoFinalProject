using Application.Features.LectureViews.Constants;
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

namespace Application.Features.LectureViews.Commands.Delete;

public class DeleteLectureViewCommand : IRequest<DeletedLectureViewResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureViewsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureViews";

    public class DeleteLectureViewCommandHandler : IRequestHandler<DeleteLectureViewCommand, DeletedLectureViewResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureViewRepository _lectureViewRepository;
        private readonly LectureViewBusinessRules _lectureViewBusinessRules;

        public DeleteLectureViewCommandHandler(IMapper mapper, ILectureViewRepository lectureViewRepository,
                                         LectureViewBusinessRules lectureViewBusinessRules)
        {
            _mapper = mapper;
            _lectureViewRepository = lectureViewRepository;
            _lectureViewBusinessRules = lectureViewBusinessRules;
        }

        public async Task<DeletedLectureViewResponse> Handle(DeleteLectureViewCommand request, CancellationToken cancellationToken)
        {
            LectureView? lectureView = await _lectureViewRepository.GetAsync(predicate: lv => lv.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureViewBusinessRules.LectureViewShouldExistWhenSelected(lectureView);

            await _lectureViewRepository.DeleteAsync(lectureView!);

            DeletedLectureViewResponse response = _mapper.Map<DeletedLectureViewResponse>(lectureView);
            return response;
        }
    }
}