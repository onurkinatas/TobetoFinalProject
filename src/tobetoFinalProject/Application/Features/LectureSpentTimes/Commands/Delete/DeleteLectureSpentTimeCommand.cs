using Application.Features.LectureSpentTimes.Constants;
using Application.Features.LectureSpentTimes.Constants;
using Application.Features.LectureSpentTimes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.LectureSpentTimes.Constants.LectureSpentTimesOperationClaims;

namespace Application.Features.LectureSpentTimes.Commands.Delete;

public class DeleteLectureSpentTimeCommand : IRequest<DeletedLectureSpentTimeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureSpentTimesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureSpentTimes";

    public class DeleteLectureSpentTimeCommandHandler : IRequestHandler<DeleteLectureSpentTimeCommand, DeletedLectureSpentTimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureSpentTimeRepository _lectureSpentTimeRepository;
        private readonly LectureSpentTimeBusinessRules _lectureSpentTimeBusinessRules;

        public DeleteLectureSpentTimeCommandHandler(IMapper mapper, ILectureSpentTimeRepository lectureSpentTimeRepository,
                                         LectureSpentTimeBusinessRules lectureSpentTimeBusinessRules)
        {
            _mapper = mapper;
            _lectureSpentTimeRepository = lectureSpentTimeRepository;
            _lectureSpentTimeBusinessRules = lectureSpentTimeBusinessRules;
        }

        public async Task<DeletedLectureSpentTimeResponse> Handle(DeleteLectureSpentTimeCommand request, CancellationToken cancellationToken)
        {
            LectureSpentTime? lectureSpentTime = await _lectureSpentTimeRepository.GetAsync(predicate: lst => lst.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureSpentTimeBusinessRules.LectureSpentTimeShouldExistWhenSelected(lectureSpentTime);

            await _lectureSpentTimeRepository.DeleteAsync(lectureSpentTime!);

            DeletedLectureSpentTimeResponse response = _mapper.Map<DeletedLectureSpentTimeResponse>(lectureSpentTime);
            return response;
        }
    }
}