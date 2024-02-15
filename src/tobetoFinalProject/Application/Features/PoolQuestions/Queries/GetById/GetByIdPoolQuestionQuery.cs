using Application.Features.PoolQuestions.Constants;
using Application.Features.PoolQuestions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.PoolQuestions.Constants.PoolQuestionsOperationClaims;

namespace Application.Features.PoolQuestions.Queries.GetById;

public class GetByIdPoolQuestionQuery : IRequest<GetByIdPoolQuestionResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdPoolQuestionQueryHandler : IRequestHandler<GetByIdPoolQuestionQuery, GetByIdPoolQuestionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPoolQuestionRepository _poolQuestionRepository;
        private readonly PoolQuestionBusinessRules _poolQuestionBusinessRules;

        public GetByIdPoolQuestionQueryHandler(IMapper mapper, IPoolQuestionRepository poolQuestionRepository, PoolQuestionBusinessRules poolQuestionBusinessRules)
        {
            _mapper = mapper;
            _poolQuestionRepository = poolQuestionRepository;
            _poolQuestionBusinessRules = poolQuestionBusinessRules;
        }

        public async Task<GetByIdPoolQuestionResponse> Handle(GetByIdPoolQuestionQuery request, CancellationToken cancellationToken)
        {
            PoolQuestion? poolQuestion = await _poolQuestionRepository.GetAsync(predicate: pq => pq.Id == request.Id, cancellationToken: cancellationToken);
            await _poolQuestionBusinessRules.PoolQuestionShouldExistWhenSelected(poolQuestion);

            GetByIdPoolQuestionResponse response = _mapper.Map<GetByIdPoolQuestionResponse>(poolQuestion);
            return response;
        }
    }
}