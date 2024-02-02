using Application.Features.ContentInstructors.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentInstructors.Constants.ContentInstructorsOperationClaims;

namespace Application.Features.ContentInstructors.Queries.GetList;

public class GetListContentInstructorQuery : IRequest<GetListResponse<GetListContentInstructorListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentInstructors({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentInstructors";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentInstructorQueryHandler : IRequestHandler<GetListContentInstructorQuery, GetListResponse<GetListContentInstructorListItemDto>>
    {
        private readonly IContentInstructorRepository _contentInstructorRepository;
        private readonly IMapper _mapper;

        public GetListContentInstructorQueryHandler(IContentInstructorRepository contentInstructorRepository, IMapper mapper)
        {
            _contentInstructorRepository = contentInstructorRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentInstructorListItemDto>> Handle(GetListContentInstructorQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentInstructor> contentInstructors = await _contentInstructorRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentInstructorListItemDto> response = _mapper.Map<GetListResponse<GetListContentInstructorListItemDto>>(contentInstructors);
            return response;
        }
    }
}