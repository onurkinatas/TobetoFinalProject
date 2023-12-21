using Application.Features.LanguageLevels.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.LanguageLevels.Constants.LanguageLevelsOperationClaims;

namespace Application.Features.LanguageLevels.Queries.GetList;

public class GetListLanguageLevelQuery : IRequest<GetListResponse<GetListLanguageLevelListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLanguageLevels({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLanguageLevels";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLanguageLevelQueryHandler : IRequestHandler<GetListLanguageLevelQuery, GetListResponse<GetListLanguageLevelListItemDto>>
    {
        private readonly ILanguageLevelRepository _languageLevelRepository;
        private readonly IMapper _mapper;

        public GetListLanguageLevelQueryHandler(ILanguageLevelRepository languageLevelRepository, IMapper mapper)
        {
            _languageLevelRepository = languageLevelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLanguageLevelListItemDto>> Handle(GetListLanguageLevelQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LanguageLevel> languageLevels = await _languageLevelRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLanguageLevelListItemDto> response = _mapper.Map<GetListResponse<GetListLanguageLevelListItemDto>>(languageLevels);
            return response;
        }
    }
}