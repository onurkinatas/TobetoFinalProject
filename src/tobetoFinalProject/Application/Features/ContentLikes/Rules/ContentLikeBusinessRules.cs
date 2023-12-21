using Application.Features.ContentLikes.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContentLikes.Rules;

public class ContentLikeBusinessRules : BaseBusinessRules
{
    private readonly IContentLikeRepository _contentLikeRepository;

    public ContentLikeBusinessRules(IContentLikeRepository contentLikeRepository)
    {
        _contentLikeRepository = contentLikeRepository;
    }

    public Task ContentLikeShouldExistWhenSelected(ContentLike? contentLike)
    {
        if (contentLike == null)
            throw new BusinessException(ContentLikesBusinessMessages.ContentLikeNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentLikeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ContentLike? contentLike = await _contentLikeRepository.GetAsync(
            predicate: cl => cl.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentLikeShouldExistWhenSelected(contentLike);
    }
}