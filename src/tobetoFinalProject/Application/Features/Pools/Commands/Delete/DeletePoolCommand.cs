using Application.Features.Pools.Constants;
using Application.Features.Pools.Constants;
using Application.Features.Pools.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Pools.Constants.PoolsOperationClaims;

namespace Application.Features.Pools.Commands.Delete;

public class DeletePoolCommand : IRequest<DeletedPoolResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, PoolsOperationClaims.Delete };

    public class DeletePoolCommandHandler : IRequestHandler<DeletePoolCommand, DeletedPoolResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPoolRepository _poolRepository;
        private readonly PoolBusinessRules _poolBusinessRules;

        public DeletePoolCommandHandler(IMapper mapper, IPoolRepository poolRepository,
                                         PoolBusinessRules poolBusinessRules)
        {
            _mapper = mapper;
            _poolRepository = poolRepository;
            _poolBusinessRules = poolBusinessRules;
        }

        public async Task<DeletedPoolResponse> Handle(DeletePoolCommand request, CancellationToken cancellationToken)
        {
            Pool? pool = await _poolRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _poolBusinessRules.PoolShouldExistWhenSelected(pool);

            await _poolRepository.DeleteAsync(pool!);

            DeletedPoolResponse response = _mapper.Map<DeletedPoolResponse>(pool);
            return response;
        }
    }
}