using Application.Features.Appeals.Constants;
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

namespace Application.Features.Appeals.Commands.Delete;

public class DeleteAppealCommand : IRequest<DeletedAppealResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, AppealsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAppeals";

    public class DeleteAppealCommandHandler : IRequestHandler<DeleteAppealCommand, DeletedAppealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppealRepository _appealRepository;
        private readonly AppealBusinessRules _appealBusinessRules;

        public DeleteAppealCommandHandler(IMapper mapper, IAppealRepository appealRepository,
                                         AppealBusinessRules appealBusinessRules)
        {
            _mapper = mapper;
            _appealRepository = appealRepository;
            _appealBusinessRules = appealBusinessRules;
        }

        public async Task<DeletedAppealResponse> Handle(DeleteAppealCommand request, CancellationToken cancellationToken)
        {
            Appeal? appeal = await _appealRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _appealBusinessRules.AppealShouldExistWhenSelected(appeal);

            await _appealRepository.DeleteAsync(appeal!);

            DeletedAppealResponse response = _mapper.Map<DeletedAppealResponse>(appeal);
            return response;
        }
    }
}