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

namespace Application.Features.ContentTags.Commands.Create;

public class CreateContentTagCommand : IRequest<CreatedContentTagResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid ContentId { get; set; }
    public Guid TagId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentTagsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentTags";

    public class CreateContentTagCommandHandler : IRequestHandler<CreateContentTagCommand, CreatedContentTagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentTagRepository _contentTagRepository;
        private readonly ContentTagBusinessRules _contentTagBusinessRules;

        public CreateContentTagCommandHandler(IMapper mapper, IContentTagRepository contentTagRepository,
                                         ContentTagBusinessRules contentTagBusinessRules)
        {
            _mapper = mapper;
            _contentTagRepository = contentTagRepository;
            _contentTagBusinessRules = contentTagBusinessRules;
        }

        public async Task<CreatedContentTagResponse> Handle(CreateContentTagCommand request, CancellationToken cancellationToken)
        {
            ContentTag contentTag = _mapper.Map<ContentTag>(request);

            await _contentTagRepository.AddAsync(contentTag);

            CreatedContentTagResponse response = _mapper.Map<CreatedContentTagResponse>(contentTag);
            return response;
        }
    }
}