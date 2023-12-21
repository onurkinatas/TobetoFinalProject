using Application.Features.ContentTags.Constants;
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

namespace Application.Features.ContentTags.Commands.Delete;

public class DeleteContentTagCommand : IRequest<DeletedContentTagResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentTagsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentTags";

    public class DeleteContentTagCommandHandler : IRequestHandler<DeleteContentTagCommand, DeletedContentTagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentTagRepository _contentTagRepository;
        private readonly ContentTagBusinessRules _contentTagBusinessRules;

        public DeleteContentTagCommandHandler(IMapper mapper, IContentTagRepository contentTagRepository,
                                         ContentTagBusinessRules contentTagBusinessRules)
        {
            _mapper = mapper;
            _contentTagRepository = contentTagRepository;
            _contentTagBusinessRules = contentTagBusinessRules;
        }

        public async Task<DeletedContentTagResponse> Handle(DeleteContentTagCommand request, CancellationToken cancellationToken)
        {
            ContentTag? contentTag = await _contentTagRepository.GetAsync(predicate: ct => ct.Id == request.Id, cancellationToken: cancellationToken);
            await _contentTagBusinessRules.ContentTagShouldExistWhenSelected(contentTag);

            await _contentTagRepository.DeleteAsync(contentTag!);

            DeletedContentTagResponse response = _mapper.Map<DeletedContentTagResponse>(contentTag);
            return response;
        }
    }
}