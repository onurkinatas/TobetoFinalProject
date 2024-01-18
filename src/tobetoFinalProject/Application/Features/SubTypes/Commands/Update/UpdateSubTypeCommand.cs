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

namespace Application.Features.SubTypes.Commands.Update;

public class UpdateSubTypeCommand : IRequest<UpdatedSubTypeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, SubTypesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetSubTypes";

    public class UpdateSubTypeCommandHandler : IRequestHandler<UpdateSubTypeCommand, UpdatedSubTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubTypeRepository _subTypeRepository;
        private readonly SubTypeBusinessRules _subTypeBusinessRules;

        public UpdateSubTypeCommandHandler(IMapper mapper, ISubTypeRepository subTypeRepository,
                                         SubTypeBusinessRules subTypeBusinessRules)
        {
            _mapper = mapper;
            _subTypeRepository = subTypeRepository;
            _subTypeBusinessRules = subTypeBusinessRules;
        }

        public async Task<UpdatedSubTypeResponse> Handle(UpdateSubTypeCommand request, CancellationToken cancellationToken)
        {
            SubType? subType = await _subTypeRepository.GetAsync(predicate: st => st.Id == request.Id, cancellationToken: cancellationToken);
            await _subTypeBusinessRules.SubTypeShouldExistWhenSelected(subType);
            subType = _mapper.Map(request, subType);

            await _subTypeRepository.UpdateAsync(subType!);

            await _subTypeBusinessRules.SubTypeNameShouldNotExist(subType, cancellationToken);

            UpdatedSubTypeResponse response = _mapper.Map<UpdatedSubTypeResponse>(subType);
            return response;
        }
    }
}