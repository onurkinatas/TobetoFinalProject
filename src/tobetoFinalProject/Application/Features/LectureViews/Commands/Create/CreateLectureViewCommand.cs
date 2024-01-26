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

namespace Application.Features.LectureViews.Commands.Create;

public class CreateLectureViewCommand : IRequest<CreatedLectureViewResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public Guid ContentId { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureViewsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureViews";

    public class CreateLectureViewCommandHandler : IRequestHandler<CreateLectureViewCommand, CreatedLectureViewResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureViewRepository _lectureViewRepository;
        private readonly LectureViewBusinessRules _lectureViewBusinessRules;

        public CreateLectureViewCommandHandler(IMapper mapper, ILectureViewRepository lectureViewRepository,
                                         LectureViewBusinessRules lectureViewBusinessRules)
        {
            _mapper = mapper;
            _lectureViewRepository = lectureViewRepository;
            _lectureViewBusinessRules = lectureViewBusinessRules;
        }

        public async Task<CreatedLectureViewResponse> Handle(CreateLectureViewCommand request, CancellationToken cancellationToken)
        {
            LectureView lectureView = _mapper.Map<LectureView>(request);

            await _lectureViewRepository.AddAsync(lectureView);

            CreatedLectureViewResponse response = _mapper.Map<CreatedLectureViewResponse>(lectureView);
            return response;
        }
    }
}