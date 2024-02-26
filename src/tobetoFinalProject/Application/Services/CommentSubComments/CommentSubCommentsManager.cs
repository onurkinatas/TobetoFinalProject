using Application.Features.CommentSubComments.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CommentSubComments;

public class CommentSubCommentsManager : ICommentSubCommentsService
{
    private readonly ICommentSubCommentRepository _commentSubCommentRepository;
    private readonly CommentSubCommentBusinessRules _commentSubCommentBusinessRules;

    public CommentSubCommentsManager(ICommentSubCommentRepository commentSubCommentRepository, CommentSubCommentBusinessRules commentSubCommentBusinessRules)
    {
        _commentSubCommentRepository = commentSubCommentRepository;
        _commentSubCommentBusinessRules = commentSubCommentBusinessRules;
    }

    public async Task<CommentSubComment?> GetAsync(
        Expression<Func<CommentSubComment, bool>> predicate,
        Func<IQueryable<CommentSubComment>, IIncludableQueryable<CommentSubComment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        CommentSubComment? commentSubComment = await _commentSubCommentRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return commentSubComment;
    }

    public async Task<IPaginate<CommentSubComment>?> GetListAsync(
        Expression<Func<CommentSubComment, bool>>? predicate = null,
        Func<IQueryable<CommentSubComment>, IOrderedQueryable<CommentSubComment>>? orderBy = null,
        Func<IQueryable<CommentSubComment>, IIncludableQueryable<CommentSubComment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<CommentSubComment> commentSubCommentList = await _commentSubCommentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return commentSubCommentList;
    }

    public async Task<CommentSubComment> AddAsync(CommentSubComment commentSubComment)
    {
        CommentSubComment addedCommentSubComment = await _commentSubCommentRepository.AddAsync(commentSubComment);

        return addedCommentSubComment;
    }

    public async Task<CommentSubComment> UpdateAsync(CommentSubComment commentSubComment)
    {
        CommentSubComment updatedCommentSubComment = await _commentSubCommentRepository.UpdateAsync(commentSubComment);

        return updatedCommentSubComment;
    }

    public async Task<CommentSubComment> DeleteAsync(CommentSubComment commentSubComment, bool permanent = false)
    {
        CommentSubComment deletedCommentSubComment = await _commentSubCommentRepository.DeleteAsync(commentSubComment);

        return deletedCommentSubComment;
    }
}
