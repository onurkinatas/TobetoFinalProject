using Application.Features.Pools.Constants;
using Application.Features.Pools.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Pools.Constants.PoolsOperationClaims;

namespace Application.Features.Pools.Queries.GetById;

public class GetByIdPoolQuery : IRequest<GetByIdPoolResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdPoolQueryHandler : IRequestHandler<GetByIdPoolQuery, GetByIdPoolResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPoolRepository _poolRepository;
        private readonly PoolBusinessRules _poolBusinessRules;

        public GetByIdPoolQueryHandler(IMapper mapper, IPoolRepository poolRepository, PoolBusinessRules poolBusinessRules)
        {
            _mapper = mapper;
            _poolRepository = poolRepository;
            _poolBusinessRules = poolBusinessRules;
        }

        public async Task<GetByIdPoolResponse> Handle(GetByIdPoolQuery request, CancellationToken cancellationToken)
        {
            Pool? pool = await _poolRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _poolBusinessRules.PoolShouldExistWhenSelected(pool);

            GetByIdPoolResponse response = _mapper.Map<GetByIdPoolResponse>(pool);
            return response;
        }
    }
}