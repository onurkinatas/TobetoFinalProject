using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class LectureLikeRepository : EfRepositoryBase<LectureLike, Guid, BaseDbContext>, ILectureLikeRepository
{
    public LectureLikeRepository(BaseDbContext context) : base(context)
    {
    }
    public int GetLectureLikeCount(Expression<Func<LectureLike, bool>> filter) => Context.Set<LectureLike>().Where(e => e.DeletedDate == null).Where(filter).ToList().Count;
}