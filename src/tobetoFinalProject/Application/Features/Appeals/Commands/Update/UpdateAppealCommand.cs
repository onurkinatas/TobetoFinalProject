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
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appeals.Commands.Update;

public class UpdateAppealCommand : IRequest<UpdatedAppealResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public string[] Roles => new[] { Admin, Write, AppealsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAppeals";

    public class UpdateAppealCommandHandler : IRequestHandler<UpdateAppealCommand, UpdatedAppealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppealRepository _appealRepository;
        private readonly AppealBusinessRules _appealBusinessRules;

        public UpdateAppealCommandHandler(IMapper mapper, IAppealRepository appealRepository,
                                         AppealBusinessRules appealBusinessRules)
        {
            _mapper = mapper;
            _appealRepository = appealRepository;
            _appealBusinessRules = appealBusinessRules;
        }

        public async Task<UpdatedAppealResponse> Handle(UpdateAppealCommand request, CancellationToken cancellationToken)
        {
            Appeal? appeal = await _appealRepository.GetAsync(
                predicate: a => a.Id == request.Id,
                include: c => c.Include(c => c.AppealStages),
                cancellationToken: cancellationToken);
            await _appealBusinessRules.AppealShouldExistWhenSelected(appeal);
            appeal = _mapper.Map(request, appeal);

            await _appealRepository.UpdateAsync(appeal!);

            UpdatedAppealResponse response = _mapper.Map<UpdatedAppealResponse>(appeal);
            return response;
        }
    }
}