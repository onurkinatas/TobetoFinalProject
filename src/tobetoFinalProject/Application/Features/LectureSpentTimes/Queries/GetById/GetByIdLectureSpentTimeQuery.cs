using Application.Features.LectureSpentTimes.Constants;
using Application.Features.LectureSpentTimes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.LectureSpentTimes.Constants.LectureSpentTimesOperationClaims;

namespace Application.Features.LectureSpentTimes.Queries.GetById;

public class GetByIdLectureSpentTimeQuery : IRequest<GetByIdLectureSpentTimeResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdLectureSpentTimeQueryHandler : IRequestHandler<GetByIdLectureSpentTimeQuery, GetByIdLectureSpentTimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureSpentTimeRepository _lectureSpentTimeRepository;
        private readonly LectureSpentTimeBusinessRules _lectureSpentTimeBusinessRules;

        public GetByIdLectureSpentTimeQueryHandler(IMapper mapper, ILectureSpentTimeRepository lectureSpentTimeRepository, LectureSpentTimeBusinessRules lectureSpentTimeBusinessRules)
        {
            _mapper = mapper;
            _lectureSpentTimeRepository = lectureSpentTimeRepository;
            _lectureSpentTimeBusinessRules = lectureSpentTimeBusinessRules;
        }

        public async Task<GetByIdLectureSpentTimeResponse> Handle(GetByIdLectureSpentTimeQuery request, CancellationToken cancellationToken)
        {
            LectureSpentTime? lectureSpentTime = await _lectureSpentTimeRepository.GetAsync(predicate: lst => lst.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureSpentTimeBusinessRules.LectureSpentTimeShouldExistWhenSelected(lectureSpentTime);

            GetByIdLectureSpentTimeResponse response = _mapper.Map<GetByIdLectureSpentTimeResponse>(lectureSpentTime);
            return response;
        }
    }
}