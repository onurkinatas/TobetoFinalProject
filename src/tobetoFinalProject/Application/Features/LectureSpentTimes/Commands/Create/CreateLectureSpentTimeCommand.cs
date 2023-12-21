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

namespace Application.Features.LectureSpentTimes.Commands.Create;

public class CreateLectureSpentTimeCommand : IRequest<CreatedLectureSpentTimeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public double SpentedTime { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureSpentTimesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureSpentTimes";

    public class CreateLectureSpentTimeCommandHandler : IRequestHandler<CreateLectureSpentTimeCommand, CreatedLectureSpentTimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureSpentTimeRepository _lectureSpentTimeRepository;
        private readonly LectureSpentTimeBusinessRules _lectureSpentTimeBusinessRules;

        public CreateLectureSpentTimeCommandHandler(IMapper mapper, ILectureSpentTimeRepository lectureSpentTimeRepository,
                                         LectureSpentTimeBusinessRules lectureSpentTimeBusinessRules)
        {
            _mapper = mapper;
            _lectureSpentTimeRepository = lectureSpentTimeRepository;
            _lectureSpentTimeBusinessRules = lectureSpentTimeBusinessRules;
        }

        public async Task<CreatedLectureSpentTimeResponse> Handle(CreateLectureSpentTimeCommand request, CancellationToken cancellationToken)
        {
            LectureSpentTime lectureSpentTime = _mapper.Map<LectureSpentTime>(request);

            await _lectureSpentTimeRepository.AddAsync(lectureSpentTime);

            CreatedLectureSpentTimeResponse response = _mapper.Map<CreatedLectureSpentTimeResponse>(lectureSpentTime);
            return response;
        }
    }
}