using Application.Features.LectureCompletionConditions.Queries.GetList;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
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

namespace Application.Features.LectureCompletionConditions.Queries.GetListLecturesForCompleted;
public class GetListLectureCompletionConditionForCompletedQuery : IRequest<GetListResponse<GetListLectureCompletionConditionListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLectureCompletionConditions({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLectureCompletionConditions";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLectureCompletionConditionForCompletedQueryHandler : IRequestHandler<GetListLectureCompletionConditionForCompletedQuery, GetListResponse<GetListLectureCompletionConditionListItemDto>>
    {
        private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;
        public GetListLectureCompletionConditionForCompletedQueryHandler(ILectureCompletionConditionRepository lectureCompletionConditionRepository, IMapper mapper, IContextOperationService contextOperationService)
        {
            _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
            _mapper = mapper;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListLectureCompletionConditionListItemDto>> Handle(GetListLectureCompletionConditionForCompletedQuery request, CancellationToken cancellationToken)
        {
            Student getStudent =await _contextOperationService.GetStudentFromContext();
            IPaginate<LectureCompletionCondition> lectureCompletionConditions = await _lectureCompletionConditionRepository.GetListAsync(
                predicate:lcc=>lcc.StudentId==getStudent.Id&&lcc.CompletionPercentage==100,
                include: lcc => lcc.Include(lcc => lcc.Student)
                .ThenInclude(s => s.User)
                .Include(lcc => lcc.Lecture),

                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLectureCompletionConditionListItemDto> response = _mapper.Map<GetListResponse<GetListLectureCompletionConditionListItemDto>>(lectureCompletionConditions);
            return response;
        }
    }
}
