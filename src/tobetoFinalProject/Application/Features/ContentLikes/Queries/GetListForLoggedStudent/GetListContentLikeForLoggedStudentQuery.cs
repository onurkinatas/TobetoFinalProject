using Application.Features.ContentLikes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentLikes.Constants.ContentLikesOperationClaims;
using Application.Services.ContextOperations;
using Microsoft.EntityFrameworkCore;
using Application.Features.LectureLikes.Queries.GetListForLoggedStudent;

namespace Application.Features.ContentLikes.Queries.GetListForLoggedStudent;

public class GetListContentLikeForLoggedStudentQuery : IRequest<GetListResponse<GetListContentLikeForLoggedStudentListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read,"Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentLikes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentLikes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentLikeQueryHandler : IRequestHandler<GetListContentLikeForLoggedStudentQuery, GetListResponse<GetListContentLikeForLoggedStudentListItemDto>>
    {
        private readonly IContentLikeRepository _contentLikeRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;

        public GetListContentLikeQueryHandler(IContentLikeRepository contentLikeRepository, IMapper mapper, IContextOperationService contextOperationService)
        {
            _contentLikeRepository = contentLikeRepository;
            _mapper = mapper;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListContentLikeForLoggedStudentListItemDto>> Handle(GetListContentLikeForLoggedStudentQuery request, CancellationToken cancellationToken)
        {
            Student student = await _contextOperationService.GetStudentFromContext();

            IPaginate<ContentLike> contentLikes = await _contentLikeRepository.GetListAsync(
                predicate:cl=>cl.StudentId == student.Id,
                include:cl=>cl.Include(cl=>cl.Content),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentLikeForLoggedStudentListItemDto> response = _mapper.Map<GetListResponse<GetListContentLikeForLoggedStudentListItemDto>>(contentLikes);

            return response;

        }
    }
}