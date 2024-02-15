using Application.Features.PoolQuestions.Constants;
using Application.Features.PoolQuestions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.PoolQuestions.Constants.PoolQuestionsOperationClaims;

namespace Application.Features.PoolQuestions.Commands.Create;

public class CreatePoolQuestionCommand : IRequest<CreatedPoolQuestionResponse>, /*ISecuredRequest,*/ ILoggableRequest, ITransactionalRequest
{
    public int PoolId { get; set; }
    public int QuestionId { get; set; }

    public string[] Roles => new[] { Admin, Write, PoolQuestionsOperationClaims.Create };

    public class CreatePoolQuestionCommandHandler : IRequestHandler<CreatePoolQuestionCommand, CreatedPoolQuestionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPoolQuestionRepository _poolQuestionRepository;
        private readonly PoolQuestionBusinessRules _poolQuestionBusinessRules;

        public CreatePoolQuestionCommandHandler(IMapper mapper, IPoolQuestionRepository poolQuestionRepository,
                                         PoolQuestionBusinessRules poolQuestionBusinessRules)
        {
            _mapper = mapper;
            _poolQuestionRepository = poolQuestionRepository;
            _poolQuestionBusinessRules = poolQuestionBusinessRules;
        }

        public async Task<CreatedPoolQuestionResponse> Handle(CreatePoolQuestionCommand request, CancellationToken cancellationToken)
        {
            PoolQuestion poolQuestion = _mapper.Map<PoolQuestion>(request);

            await _poolQuestionRepository.AddAsync(poolQuestion);

            CreatedPoolQuestionResponse response = _mapper.Map<CreatedPoolQuestionResponse>(poolQuestion);
            return response;
        }
    }
}