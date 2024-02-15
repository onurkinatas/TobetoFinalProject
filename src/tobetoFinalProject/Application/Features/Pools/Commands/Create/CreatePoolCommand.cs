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

namespace Application.Features.Pools.Commands.Create;

public class CreatePoolCommand : IRequest<CreatedPoolResponse>,/* ISecuredRequest,*/ ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, PoolsOperationClaims.Create };

    public class CreatePoolCommandHandler : IRequestHandler<CreatePoolCommand, CreatedPoolResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPoolRepository _poolRepository;
        private readonly PoolBusinessRules _poolBusinessRules;

        public CreatePoolCommandHandler(IMapper mapper, IPoolRepository poolRepository,
                                         PoolBusinessRules poolBusinessRules)
        {
            _mapper = mapper;
            _poolRepository = poolRepository;
            _poolBusinessRules = poolBusinessRules;
        }

        public async Task<CreatedPoolResponse> Handle(CreatePoolCommand request, CancellationToken cancellationToken)
        {
            Pool pool = _mapper.Map<Pool>(request);

            await _poolRepository.AddAsync(pool);

            CreatedPoolResponse response = _mapper.Map<CreatedPoolResponse>(pool);
            return response;
        }
    }
}