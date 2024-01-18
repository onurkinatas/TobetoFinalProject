using Application.Features.SubTypes.Constants;
using Application.Features.SubTypes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.SubTypes.Constants.SubTypesOperationClaims;
using Application.Features.Languages.Rules;

namespace Application.Features.SubTypes.Commands.Create;

public class CreateSubTypeCommand : IRequest<CreatedSubTypeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, SubTypesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetSubTypes";

    public class CreateSubTypeCommandHandler : IRequestHandler<CreateSubTypeCommand, CreatedSubTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubTypeRepository _subTypeRepository;
        private readonly SubTypeBusinessRules _subTypeBusinessRules;

        public CreateSubTypeCommandHandler(IMapper mapper, ISubTypeRepository subTypeRepository,
                                         SubTypeBusinessRules subTypeBusinessRules)
        {
            _mapper = mapper;
            _subTypeRepository = subTypeRepository;
            _subTypeBusinessRules = subTypeBusinessRules;
        }

        public async Task<CreatedSubTypeResponse> Handle(CreateSubTypeCommand request, CancellationToken cancellationToken)
        {
            SubType subType = _mapper.Map<SubType>(request);

            await _subTypeRepository.AddAsync(subType);

            CreatedSubTypeResponse response = _mapper.Map<CreatedSubTypeResponse>(subType);

            await _subTypeBusinessRules.SubTypeNameShouldNotExist(subType, cancellationToken);

            return response;
        }
    }
}