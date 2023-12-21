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

namespace Application.Features.ContentLikes.Commands.Update;

public class UpdateContentLikeCommand : IRequest<UpdatedContentLikeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public bool IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid ContentId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentLikesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentLikes";

    public class UpdateContentLikeCommandHandler : IRequestHandler<UpdateContentLikeCommand, UpdatedContentLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentLikeRepository _contentLikeRepository;
        private readonly ContentLikeBusinessRules _contentLikeBusinessRules;

        public UpdateContentLikeCommandHandler(IMapper mapper, IContentLikeRepository contentLikeRepository,
                                         ContentLikeBusinessRules contentLikeBusinessRules)
        {
            _mapper = mapper;
            _contentLikeRepository = contentLikeRepository;
            _contentLikeBusinessRules = contentLikeBusinessRules;
        }

        public async Task<UpdatedContentLikeResponse> Handle(UpdateContentLikeCommand request, CancellationToken cancellationToken)
        {
            ContentLike? contentLike = await _contentLikeRepository.GetAsync(predicate: cl => cl.Id == request.Id, cancellationToken: cancellationToken);
            await _contentLikeBusinessRules.ContentLikeShouldExistWhenSelected(contentLike);
            contentLike = _mapper.Map(request, contentLike);

            await _contentLikeRepository.UpdateAsync(contentLike!);

            UpdatedContentLikeResponse response = _mapper.Map<UpdatedContentLikeResponse>(contentLike);
            return response;
        }
    }
}