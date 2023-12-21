using Application.Features.SubTypes.Constants;
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

namespace Application.Features.SubTypes.Commands.Delete;

public class DeleteSubTypeCommand : IRequest<DeletedSubTypeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, SubTypesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetSubTypes";

    public class DeleteSubTypeCommandHandler : IRequestHandler<DeleteSubTypeCommand, DeletedSubTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubTypeRepository _subTypeRepository;
        private readonly SubTypeBusinessRules _subTypeBusinessRules;

        public DeleteSubTypeCommandHandler(IMapper mapper, ISubTypeRepository subTypeRepository,
                                         SubTypeBusinessRules subTypeBusinessRules)
        {
            _mapper = mapper;
            _subTypeRepository = subTypeRepository;
            _subTypeBusinessRules = subTypeBusinessRules;
        }

        public async Task<DeletedSubTypeResponse> Handle(DeleteSubTypeCommand request, CancellationToken cancellationToken)
        {
            SubType? subType = await _subTypeRepository.GetAsync(predicate: st => st.Id == request.Id, cancellationToken: cancellationToken);
            await _subTypeBusinessRules.SubTypeShouldExistWhenSelected(subType);

            await _subTypeRepository.DeleteAsync(subType!);

            DeletedSubTypeResponse response = _mapper.Map<DeletedSubTypeResponse>(subType);
            return response;
        }
    }
}