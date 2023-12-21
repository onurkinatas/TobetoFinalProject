using Application.Features.ContentTags.Constants;
using Application.Features.ContentTags.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentTags.Constants.ContentTagsOperationClaims;

namespace Application.Features.ContentTags.Commands.Update;

public class UpdateContentTagCommand : IRequest<UpdatedContentTagResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid ContentId { get; set; }
    public Guid TagId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentTagsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentTags";

    public class UpdateContentTagCommandHandler : IRequestHandler<UpdateContentTagCommand, UpdatedContentTagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentTagRepository _contentTagRepository;
        private readonly ContentTagBusinessRules _contentTagBusinessRules;

        public UpdateContentTagCommandHandler(IMapper mapper, IContentTagRepository contentTagRepository,
                                         ContentTagBusinessRules contentTagBusinessRules)
        {
            _mapper = mapper;
            _contentTagRepository = contentTagRepository;
            _contentTagBusinessRules = contentTagBusinessRules;
        }

        public async Task<UpdatedContentTagResponse> Handle(UpdateContentTagCommand request, CancellationToken cancellationToken)
        {
            ContentTag? contentTag = await _contentTagRepository.GetAsync(predicate: ct => ct.Id == request.Id, cancellationToken: cancellationToken);
            await _contentTagBusinessRules.ContentTagShouldExistWhenSelected(contentTag);
            contentTag = _mapper.Map(request, contentTag);

            await _contentTagRepository.UpdateAsync(contentTag!);

            UpdatedContentTagResponse response = _mapper.Map<UpdatedContentTagResponse>(contentTag);
            return response;
        }
    }
}