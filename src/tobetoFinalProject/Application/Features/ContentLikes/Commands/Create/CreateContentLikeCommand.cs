using Application.Features.ContentLikes.Constants;
using Application.Features.ContentLikes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentLikes.Constants.ContentLikesOperationClaims;

namespace Application.Features.ContentLikes.Commands.Create;

public class CreateContentLikeCommand : IRequest<CreatedContentLikeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public bool IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid ContentId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentLikesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentLikes";

    public class CreateContentLikeCommandHandler : IRequestHandler<CreateContentLikeCommand, CreatedContentLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentLikeRepository _contentLikeRepository;
        private readonly ContentLikeBusinessRules _contentLikeBusinessRules;

        public CreateContentLikeCommandHandler(IMapper mapper, IContentLikeRepository contentLikeRepository,
                                         ContentLikeBusinessRules contentLikeBusinessRules)
        {
            _mapper = mapper;
            _contentLikeRepository = contentLikeRepository;
            _contentLikeBusinessRules = contentLikeBusinessRules;
        }

        public async Task<CreatedContentLikeResponse> Handle(CreateContentLikeCommand request, CancellationToken cancellationToken)
        {
            ContentLike contentLike = _mapper.Map<ContentLike>(request);

            await _contentLikeRepository.AddAsync(contentLike);

            CreatedContentLikeResponse response = _mapper.Map<CreatedContentLikeResponse>(contentLike);
            return response;
        }
    }
}