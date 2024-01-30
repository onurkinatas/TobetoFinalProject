using Application.Features.LectureViews.Queries.GetList;
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

namespace Application.Features.LectureViews.Queries.GetListByLectureAndContentId;
public class GetListByLectureAndContentIdLectureViewQuery : IRequest<GetListResponse<GetListLectureViewListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid LectureId { get; set; }
    public Guid ContentId { get; set; }

    public string[] Roles => new[] { "Admin" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLectureViews({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLectureViews";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByLectureAndContentIdLectureViewQueryHandler : IRequestHandler<GetListByLectureAndContentIdLectureViewQuery, GetListResponse<GetListLectureViewListItemDto>>
    {
        private readonly ILectureViewRepository _lectureViewRepository;
        private readonly IMapper _mapper;

        public GetListByLectureAndContentIdLectureViewQueryHandler(ILectureViewRepository lectureViewRepository, IMapper mapper)
        {
            _lectureViewRepository = lectureViewRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLectureViewListItemDto>> Handle(GetListByLectureAndContentIdLectureViewQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LectureView> lectureViews = await _lectureViewRepository.GetListAsync(
                include: lv => lv.Include(lv => lv.Student)
                .ThenInclude(s => s.User)
                .Include(lv => lv.Content)
                .Include(lv => lv.Lecture)
                ,
                predicate:lv=>lv.LectureId==request.LectureId&&lv.ContentId==request.ContentId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLectureViewListItemDto> response = _mapper.Map<GetListResponse<GetListLectureViewListItemDto>>(lectureViews);
            return response;
        }
    }
}