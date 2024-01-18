using Application.Features.Appeals.Constants;
using Application.Features.Appeals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Appeals.Constants.AppealsOperationClaims;

namespace Application.Features.Appeals.Commands.Create;

public class CreateAppealCommand : IRequest<CreatedAppealResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public string[] Roles => new[] { Admin, Write, AppealsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAppeals";

    public class CreateAppealCommandHandler : IRequestHandler<CreateAppealCommand, CreatedAppealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppealRepository _appealRepository;
        private readonly AppealBusinessRules _appealBusinessRules;

        public CreateAppealCommandHandler(IMapper mapper, IAppealRepository appealRepository,
                                         AppealBusinessRules appealBusinessRules)
        {
            _mapper = mapper;
            _appealRepository = appealRepository;
            _appealBusinessRules = appealBusinessRules;
        }

        public async Task<CreatedAppealResponse> Handle(CreateAppealCommand request, CancellationToken cancellationToken)
        {
            Appeal appeal = _mapper.Map<Appeal>(request);

            await _appealBusinessRules.AppealNameShouldNotExist(appeal, cancellationToken);

            await _appealRepository.AddAsync(appeal);

            CreatedAppealResponse response = _mapper.Map<CreatedAppealResponse>(appeal);
            return response;
        }
    }
}