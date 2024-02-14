using Application.Features.Quizs.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Quizs.Constants.QuizsOperationClaims;

namespace Application.Features.Quizs.Queries.GetList;

public class GetListQuizQuery : IRequest<GetListResponse<GetListQuizListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListQuizQueryHandler : IRequestHandler<GetListQuizQuery, GetListResponse<GetListQuizListItemDto>>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;

        public GetListQuizQueryHandler(IQuizRepository quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListQuizListItemDto>> Handle(GetListQuizQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Quiz> quizs = await _quizRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListQuizListItemDto> response = _mapper.Map<GetListResponse<GetListQuizListItemDto>>(quizs);
            return response;
        }
    }
}