using Application.Features.LectureLikes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.LectureLikes.Constants.LectureLikesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LectureLikes.Queries.GetList;

public class GetListLectureLikeQuery : IRequest<GetListResponse<GetListLectureLikeListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLectureLikes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLectureLikes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLectureLikeQueryHandler : IRequestHandler<GetListLectureLikeQuery, GetListResponse<GetListLectureLikeListItemDto>>
    {
        private readonly ILectureLikeRepository _lectureLikeRepository;
        private readonly IMapper _mapper;

        public GetListLectureLikeQueryHandler(ILectureLikeRepository lectureLikeRepository, IMapper mapper)
        {
            _lectureLikeRepository = lectureLikeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLectureLikeListItemDto>> Handle(GetListLectureLikeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LectureLike> lectureLikes = await _lectureLikeRepository.GetListAsync(
                include: ll => ll.Include(ll => ll.Student)
                .ThenInclude(s => s.User)
                .Include(ll => ll.Lecture),
                predicate: ll => ll.IsLiked==true,
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