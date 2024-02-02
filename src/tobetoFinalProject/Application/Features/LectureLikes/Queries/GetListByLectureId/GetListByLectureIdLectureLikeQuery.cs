using Application.Features.LectureLikes.Queries.GetList;
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

namespace Application.Features.LectureLikes.Queries.GetListByLectureId;
public class GetListByLectureIdLectureLikeQuery : IRequest<GetListResponse<GetListLectureLikeListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid LectureId { get; set; }

    public string[] Roles => new[] { "Admin" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLectureLikes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLectureLikes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByLectureIdLectureLikeQueryHandler : IRequestHandler<GetListByLectureIdLectureLikeQuery, GetListResponse<GetListLectureLikeListItemDto>>
    {
        private readonly ILectureLikeRepository _lectureLikeRepository;
        private readonly IMapper _mapper;

        public GetListByLectureIdLectureLikeQueryHandler(ILectureLikeRepository lectureLikeRepository, IMapper mapper)
        {
            _lectureLikeRepository = lectureLikeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLectureLikeListItemDto>> Handle(GetListByLectureIdLectureLikeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LectureLike> lectureLikes = await _lectureLikeRepository.GetListAsync(
                include: lcc => lcc.Include(lcc => lcc.Student)
                .ThenInclude(s => s.User)
                .Include(lcc => lcc.Lecture),
                predicate: lcc => lcc.IsLiked == true&&lcc.LectureId==request.LectureId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLectureLikeListItemDto> response = _mapper.Map<GetListResponse<GetListLectureLikeListItemDto>>(lectureLikes);
            return response;
        }
    }
}
