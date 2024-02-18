using Application.Features.GeneralQuizs.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.GeneralQuizs.Constants.GeneralQuizsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Core.Application.Pipelines.Caching;

namespace Application.Features.GeneralQuizs.Queries.GetList;

public class GetListGeneralQuizQuery : IRequest<GetListResponse<GetListGeneralQuizListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read,"Student" };
    public bool BypassCache { get; }
    public string CacheKey => $"GetListGeneralQuizs({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetGeneralQuizs";
    public TimeSpan? SlidingExpiration { get; }
    public class GetListGeneralQuizQueryHandler : IRequestHandler<GetListGeneralQuizQuery, GetListResponse<GetListGeneralQuizListItemDto>>
    {
        private readonly IGeneralQuizRepository _generalQuizRepository;
        private readonly IMapper _mapper;

        public GetListGeneralQuizQueryHandler(IGeneralQuizRepository generalQuizRepository, IMapper mapper)
        {
            _generalQuizRepository = generalQuizRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListGeneralQuizListItemDto>> Handle(GetListGeneralQuizQuery request, CancellationToken cancellationToken)
        {
            IPaginate<GeneralQuiz> generalQuizs = await _generalQuizRepository.GetListAsync(
                include:gq=>gq.Include(gq=>gq.Quiz),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListGeneralQuizListItemDto> response = _mapper.Map<GetListResponse<GetListGeneralQuizListItemDto>>(generalQuizs);
            return response;
        }
    }
}