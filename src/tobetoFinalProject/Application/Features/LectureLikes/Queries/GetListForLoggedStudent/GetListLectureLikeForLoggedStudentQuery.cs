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
using Application.Services.ContextOperations;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LectureLikes.Queries.GetListForLoggedStudent;

public class GetListLectureLikeForLoggedStudentQuery : IRequest<GetListResponse<GetListLectureLikeForLoggedStudentListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read,"Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLectureLikes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLectureLikes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLectureLikeQueryHandler : IRequestHandler<GetListLectureLikeForLoggedStudentQuery, GetListResponse<GetListLectureLikeForLoggedStudentListItemDto>>
    {
        private readonly ILectureLikeRepository _lectureLikeRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;

        public GetListLectureLikeQueryHandler(ILectureLikeRepository lectureLikeRepository, IMapper mapper, IContextOperationService contextOperationService)
        {
            _lectureLikeRepository = lectureLikeRepository;
            _mapper = mapper;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListLectureLikeForLoggedStudentListItemDto>> Handle(GetListLectureLikeForLoggedStudentQuery request, CancellationToken cancellationToken)
        {
            Student student = await _contextOperationService.GetStudentFromContext();

            IPaginate<LectureLike> lectureLikes = await _lectureLikeRepository.GetListAsync(
                predicate:ll=>ll.StudentId == student.Id,
                include:ll=>ll.Include(ll=>ll.Lecture),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLectureLikeForLoggedStudentListItemDto> response = _mapper.Map<GetListResponse<GetListLectureLikeForLoggedStudentListItemDto>>(lectureLikes);

            return response;

        }
    }
}