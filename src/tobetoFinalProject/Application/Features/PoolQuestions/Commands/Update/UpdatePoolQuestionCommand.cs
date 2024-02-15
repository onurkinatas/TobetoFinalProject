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

namespace Application.Features.PoolQuestions.Commands.Update;

public class UpdatePoolQuestionCommand : IRequest<UpdatedPoolQuestionResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int PoolId { get; set; }
    public int QuestionId { get; set; }

    public string[] Roles => new[] { Admin, Write, PoolQuestionsOperationClaims.Update };

    public class UpdatePoolQuestionCommandHandler : IRequestHandler<UpdatePoolQuestionCommand, UpdatedPoolQuestionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPoolQuestionRepository _poolQuestionRepository;
        private readonly PoolQuestionBusinessRules _poolQuestionBusinessRules;

        public UpdatePoolQuestionCommandHandler(IMapper mapper, IPoolQuestionRepository poolQuestionRepository,
                                         PoolQuestionBusinessRules poolQuestionBusinessRules)
        {
            _mapper = mapper;
            _poolQuestionRepository = poolQuestionRepository;
            _poolQuestionBusinessRules = poolQuestionBusinessRules;
        }

        public async Task<UpdatedPoolQuestionResponse> Handle(UpdatePoolQuestionCommand request, CancellationToken cancellationToken)
        {
            PoolQuestion? poolQuestion = await _poolQuestionRepository.GetAsync(predicate: pq => pq.Id == request.Id, cancellationToken: cancellationToken);
            await _poolQuestionBusinessRules.PoolQuestionShouldExistWhenSelected(poolQuestion);
            poolQuestion = _mapper.Map(request, poolQuestion);

            await _poolQuestionRepository.UpdateAsync(poolQuestion!);

            UpdatedPoolQuestionResponse response = _mapper.Map<UpdatedPoolQuestionResponse>(poolQuestion);
            return response;
        }
    }
}