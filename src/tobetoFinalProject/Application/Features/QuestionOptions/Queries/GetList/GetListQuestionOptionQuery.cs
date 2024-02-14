using Application.Features.QuestionOptions.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.QuestionOptions.Constants.QuestionOptionsOperationClaims;

namespace Application.Features.QuestionOptions.Queries.GetList;

public class GetListQuestionOptionQuery : IRequest<GetListResponse<GetListQuestionOptionListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListQuestionOptionQueryHandler : IRequestHandler<GetListQuestionOptionQuery, GetListResponse<GetListQuestionOptionListItemDto>>
    {
        private readonly IQuestionOptionRepository _questionOptionRepository;
        private readonly IMapper _mapper;

        public GetListQuestionOptionQueryHandler(IQuestionOptionRepository questionOptionRepository, IMapper mapper)
        {
            _questionOptionRepository = questionOptionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListQuestionOptionListItemDto>> Handle(GetListQuestionOptionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<QuestionOption> questionOptions = await _questionOptionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListQuestionOptionListItemDto> response = _mapper.Map<GetListResponse<GetListQuestionOptionListItemDto>>(questionOptions);
            return response;
        }
    }
}