using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CommentSubCommentRepository : EfRepositoryBase<CommentSubComment, int, BaseDbContext>, ICommentSubCommentRepository
{
    public CommentSubCommentRepository(BaseDbContext context) : base(context)
    {
    }
}