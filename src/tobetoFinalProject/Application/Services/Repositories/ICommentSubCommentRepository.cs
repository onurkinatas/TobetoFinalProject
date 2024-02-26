using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICommentSubCommentRepository : IAsyncRepository<CommentSubComment, int>, IRepository<CommentSubComment, int>
{
}