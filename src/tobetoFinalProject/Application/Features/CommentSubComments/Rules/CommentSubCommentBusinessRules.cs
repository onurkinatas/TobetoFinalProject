using Application.Features.CommentSubComments.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.CommentSubComments.Rules;

public class CommentSubCommentBusinessRules : BaseBusinessRules
{
    private readonly ICommentSubCommentRepository _commentSubCommentRepository;

    public CommentSubCommentBusinessRules(ICommentSubCommentRepository commentSubCommentRepository)
    {
        _commentSubCommentRepository = commentSubCommentRepository;
    }

    public Task CommentSubCommentShouldExistWhenSelected(CommentSubComment? commentSubComment)
    {
        if (commentSubComment == null)
            throw new BusinessException(CommentSubCommentsBusinessMessages.CommentSubCommentNotExists);
        return Task.CompletedTask;
    }

    public async Task CommentSubCommentIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        CommentSubComment? commentSubComment = await _commentSubCommentRepository.GetAsync(
            predicate: csc => csc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CommentSubCommentShouldExistWhenSelected(commentSubComment);
    }
}