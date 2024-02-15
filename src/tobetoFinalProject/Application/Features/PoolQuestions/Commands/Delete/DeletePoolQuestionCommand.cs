using Application.Features.PoolQuestions.Constants;
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

namespace Application.Features.PoolQuestions.Commands.Delete;

public class DeletePoolQuestionCommand : IRequest<DeletedPoolQuestionResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, PoolQuestionsOperationClaims.Delete };

    public class DeletePoolQuestionCommandHandler : IRequestHandler<DeletePoolQuestionCommand, DeletedPoolQuestionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPoolQuestionRepository _poolQuestionRepository;
        private readonly PoolQuestionBusinessRules _poolQuestionBusinessRules;

        public DeletePoolQuestionCommandHandler(IMapper mapper, IPoolQuestionRepository poolQuestionRepository,
                                         PoolQuestionBusinessRules poolQuestionBusinessRules)
        {
            _mapper = mapper;
            _poolQuestionRepository = poolQuestionRepository;
            _poolQuestionBusinessRules = poolQuestionBusinessRules;
        }

        public async Task<DeletedPoolQuestionResponse> Handle(DeletePoolQuestionCommand request, CancellationToken cancellationToken)
        {
            PoolQuestion? poolQuestion = await _poolQuestionRepository.GetAsync(predicate: pq => pq.Id == request.Id, cancellationToken: cancellationToken);
            await _poolQuestionBusinessRules.PoolQuestionShouldExistWhenSelected(poolQuestion);

            await _poolQuestionRepository.DeleteAsync(poolQuestion!);

            DeletedPoolQuestionResponse response = _mapper.Map<DeletedPoolQuestionResponse>(poolQuestion);
            return response;
        }
    }
}