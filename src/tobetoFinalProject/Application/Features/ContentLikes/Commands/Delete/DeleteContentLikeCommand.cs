using Application.Features.ContentLikes.Constants;
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

namespace Application.Features.ContentLikes.Commands.Delete;

public class DeleteContentLikeCommand : IRequest<DeletedContentLikeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentLikesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentLikes";

    public class DeleteContentLikeCommandHandler : IRequestHandler<DeleteContentLikeCommand, DeletedContentLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentLikeRepository _contentLikeRepository;
        private readonly ContentLikeBusinessRules _contentLikeBusinessRules;

        public DeleteContentLikeCommandHandler(IMapper mapper, IContentLikeRepository contentLikeRepository,
                                         ContentLikeBusinessRules contentLikeBusinessRules)
        {
            _mapper = mapper;
            _contentLikeRepository = contentLikeRepository;
            _contentLikeBusinessRules = contentLikeBusinessRules;
        }

        public async Task<DeletedContentLikeResponse> Handle(DeleteContentLikeCommand request, CancellationToken cancellationToken)
        {
            ContentLike? contentLike = await _contentLikeRepository.GetAsync(predicate: cl => cl.Id == request.Id, cancellationToken: cancellationToken);
            await _contentLikeBusinessRules.ContentLikeShouldExistWhenSelected(contentLike);

            await _contentLikeRepository.DeleteAsync(contentLike!);

            DeletedContentLikeResponse response = _mapper.Map<DeletedContentLikeResponse>(contentLike);
            return response;
        }
    }
}