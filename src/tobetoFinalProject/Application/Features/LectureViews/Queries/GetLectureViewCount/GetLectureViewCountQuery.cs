using Application.Features.LectureViews.Rules;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LectureViews.Queries.GetLectureViewCount;
public class GetLectureViewCountQuery : IRequest<GetLectureViewCountQueryResponse>, ISecuredRequest
{
    public Guid LectureId { get; set; }
    public Guid ContentId { get; set; }

    public string[] Roles => new[] { "Admin", "Student" };

    public class GetLectureViewCountQueryHandler : IRequestHandler<GetLectureViewCountQuery, GetLectureViewCountQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureViewRepository _lectureViewRepository;
        private readonly IContextOperationService _contextOperationService;
        private readonly LectureViewBusinessRules _lectureViewBusinessRules;

        public GetLectureViewCountQueryHandler(IMapper mapper, ILectureViewRepository lectureViewRepository, LectureViewBusinessRules lectureViewBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _lectureViewRepository = lectureViewRepository;
            _lectureViewBusinessRules = lectureViewBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetLectureViewCountQueryResponse> Handle(GetLectureViewCountQuery request, CancellationToken cancellationToken)
        {
            var lectureViews = await _lectureViewRepository.GetAll(cl => cl.LectureId == request.LectureId && cl.ContentId == request.ContentId);

            GetLectureViewCountQueryResponse getLectureViewCountQueryResponse = new GetLectureViewCountQueryResponse();
            getLectureViewCountQueryResponse.Count = lectureViews.Count;
            return getLectureViewCountQueryResponse;
        }
    }
}
