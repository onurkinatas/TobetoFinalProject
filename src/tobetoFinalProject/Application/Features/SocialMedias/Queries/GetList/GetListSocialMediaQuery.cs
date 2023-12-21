using Application.Features.SocialMedias.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.SocialMedias.Constants.SocialMediasOperationClaims;

namespace Application.Features.SocialMedias.Queries.GetList;

public class GetListSocialMediaQuery : IRequest<GetListResponse<GetListSocialMediaListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListSocialMedias({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetSocialMedias";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListSocialMediaQueryHandler : IRequestHandler<GetListSocialMediaQuery, GetListResponse<GetListSocialMediaListItemDto>>
    {
        private readonly ISocialMediaRepository _socialMediaRepository;
        private readonly IMapper _mapper;

        public GetListSocialMediaQueryHandler(ISocialMediaRepository socialMediaRepository, IMapper mapper)
        {
            _socialMediaRepository = socialMediaRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSocialMediaListItemDto>> Handle(GetListSocialMediaQuery request, CancellationToken cancellationToken)
        {
            IPaginate<SocialMedia> socialMedias = await _socialMediaRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSocialMediaListItemDto> response = _mapper.Map<GetListResponse<GetListSocialMediaListItemDto>>(socialMedias);
            return response;
        }
    }
}