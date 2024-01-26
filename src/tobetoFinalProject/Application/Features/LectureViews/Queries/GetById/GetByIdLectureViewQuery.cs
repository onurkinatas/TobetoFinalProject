using Application.Features.LectureViews.Constants;
using Application.Features.LectureViews.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.LectureViews.Constants.LectureViewsOperationClaims;

namespace Application.Features.LectureViews.Queries.GetById;

public class GetByIdLectureViewQuery : IRequest<GetByIdLectureViewResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdLectureViewQueryHandler : IRequestHandler<GetByIdLectureViewQuery, GetByIdLectureViewResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureViewRepository _lectureViewRepository;
        private readonly LectureViewBusinessRules _lectureViewBusinessRules;

        public GetByIdLectureViewQueryHandler(IMapper mapper, ILectureViewRepository lectureViewRepository, LectureViewBusinessRules lectureViewBusinessRules)
        {
            _mapper = mapper;
            _lectureViewRepository = lectureViewRepository;
            _lectureViewBusinessRules = lectureViewBusinessRules;
        }

        public async Task<GetByIdLectureViewResponse> Handle(GetByIdLectureViewQuery request, CancellationToken cancellationToken)
        {
            LectureView? lectureView = await _lectureViewRepository.GetAsync(predicate: lv => lv.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureViewBusinessRules.LectureViewShouldExistWhenSelected(lectureView);

            GetByIdLectureViewResponse response = _mapper.Map<GetByIdLectureViewResponse>(lectureView);
            return response;
        }
    }
}