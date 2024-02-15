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

namespace Application.Features.Pools.Commands.Update;

public class UpdatePoolCommand : IRequest<UpdatedPoolResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, PoolsOperationClaims.Update };

    public class UpdatePoolCommandHandler : IRequestHandler<UpdatePoolCommand, UpdatedPoolResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPoolRepository _poolRepository;
        private readonly PoolBusinessRules _poolBusinessRules;

        public UpdatePoolCommandHandler(IMapper mapper, IPoolRepository poolRepository,
                                         PoolBusinessRules poolBusinessRules)
        {
            _mapper = mapper;
            _poolRepository = poolRepository;
            _poolBusinessRules = poolBusinessRules;
        }

        public async Task<UpdatedPoolResponse> Handle(UpdatePoolCommand request, CancellationToken cancellationToken)
        {
            Pool? pool = await _poolRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _poolBusinessRules.PoolShouldExistWhenSelected(pool);
            pool = _mapper.Map(request, pool);

            await _poolRepository.UpdateAsync(pool!);

            UpdatedPoolResponse response = _mapper.Map<UpdatedPoolResponse>(pool);
            return response;
        }
    }
}