using Application.Features.LectureCompletionConditions.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LectureCompletionConditions.Queries.GetListByLectureId;
public class GetListByLectureIdLectureCompletionConditionQuery : IRequest<GetListResponse<GetListLectureCompletionConditionListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid LectureId { get; set; }

    public string[] Roles => new[] { "Admin","Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLectureCompletionConditions({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLectureCompletionConditions";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByLectureIdLectureCompletionConditionQueryHandler : IRequestHandler<GetListByLectureIdLectureCompletionConditionQuery, GetListResponse<GetListLectureCompletionConditionListItemDto>>
    {
        private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;
        private readonly IMapper _mapper;

        public GetListByLectureIdLectureCompletionConditionQueryHandler(ILectureCompletionConditionRepository lectureCompletionConditionRepository, IMapper mapper)
        {
            _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLectureCompletionConditionListItemDto>> Handle(GetListByLectureIdLectureCompletionConditionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LectureCompletionCondition> lectureCompletionConditions = await _lectureCompletionConditionRepository.GetListAsync(
                predicate:lcc=>lcc.LectureId==request.LectureId,
                include: lcc => lcc.Include(lcc => lcc.Student)
                .ThenInclude(s => s.User)
                .Include(lcc => lcc.Lecture),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLectureCompletionConditionListItemDto> response = _mapper.Map<GetListResponse<GetListLectureCompletionConditionListItemDto>>(lectureCompletionConditions);
            return response;
        }
    }
}
