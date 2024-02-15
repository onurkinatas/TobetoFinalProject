using Application.Features.PoolQuestions.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.PoolQuestions.Constants.PoolQuestionsOperationClaims;

namespace Application.Features.PoolQuestions.Queries.GetList;

public class GetListPoolQuestionQuery : IRequest<GetListResponse<GetListPoolQuestionListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListPoolQuestionQueryHandler : IRequestHandler<GetListPoolQuestionQuery, GetListResponse<GetListPoolQuestionListItemDto>>
    {
        private readonly IPoolQuestionRepository _poolQuestionRepository;
        private readonly IMapper _mapper;

        public GetListPoolQuestionQueryHandler(IPoolQuestionRepository poolQuestionRepository, IMapper mapper)
        {
            _poolQuestionRepository = poolQuestionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPoolQuestionListItemDto>> Handle(GetListPoolQuestionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<PoolQuestion> poolQuestions = await _poolQuestionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPoolQuestionListItemDto> response = _mapper.Map<GetListResponse<GetListPoolQuestionListItemDto>>(poolQuestions);
            return response;
        }
    }
}