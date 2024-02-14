using Application.Features.QuizQuestions.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.QuizQuestions.Constants.QuizQuestionsOperationClaims;

namespace Application.Features.QuizQuestions.Queries.GetList;

public class GetListQuizQuestionQuery : IRequest<GetListResponse<GetListQuizQuestionListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListQuizQuestionQueryHandler : IRequestHandler<GetListQuizQuestionQuery, GetListResponse<GetListQuizQuestionListItemDto>>
    {
        private readonly IQuizQuestionRepository _quizQuestionRepository;
        private readonly IMapper _mapper;

        public GetListQuizQuestionQueryHandler(IQuizQuestionRepository quizQuestionRepository, IMapper mapper)
        {
            _quizQuestionRepository = quizQuestionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListQuizQuestionListItemDto>> Handle(GetListQuizQuestionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<QuizQuestion> quizQuestions = await _quizQuestionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListQuizQuestionListItemDto> response = _mapper.Map<GetListResponse<GetListQuizQuestionListItemDto>>(quizQuestions);
            return response;
        }
    }
}