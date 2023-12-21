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

namespace Application.Features.LectureSpentTimes.Commands.Update;

public class UpdateLectureSpentTimeCommand : IRequest<UpdatedLectureSpentTimeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public double SpentedTime { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureSpentTimesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureSpentTimes";

    public class UpdateLectureSpentTimeCommandHandler : IRequestHandler<UpdateLectureSpentTimeCommand, UpdatedLectureSpentTimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureSpentTimeRepository _lectureSpentTimeRepository;
        private readonly LectureSpentTimeBusinessRules _lectureSpentTimeBusinessRules;

        public UpdateLectureSpentTimeCommandHandler(IMapper mapper, ILectureSpentTimeRepository lectureSpentTimeRepository,
                                         LectureSpentTimeBusinessRules lectureSpentTimeBusinessRules)
        {
            _mapper = mapper;
            _lectureSpentTimeRepository = lectureSpentTimeRepository;
            _lectureSpentTimeBusinessRules = lectureSpentTimeBusinessRules;
        }

        public async Task<UpdatedLectureSpentTimeResponse> Handle(UpdateLectureSpentTimeCommand request, CancellationToken cancellationToken)
        {
            LectureSpentTime? lectureSpentTime = await _lectureSpentTimeRepository.GetAsync(predicate: lst => lst.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureSpentTimeBusinessRules.LectureSpentTimeShouldExistWhenSelected(lectureSpentTime);
            lectureSpentTime = _mapper.Map(request, lectureSpentTime);

            await _lectureSpentTimeRepository.UpdateAsync(lectureSpentTime!);

            UpdatedLectureSpentTimeResponse response = _mapper.Map<UpdatedLectureSpentTimeResponse>(lectureSpentTime);
            return response;
        }
    }
}