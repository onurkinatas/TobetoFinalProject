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
    public int GetLectureLikeCount(Expression<Func<LectureLike, bool>> filter)
    =>filter!=null?Context.Set<LectureLike>().Where(filter).Count(e => e.DeletedDate == null): Context.Set<LectureLike>().Count(e => e.DeletedDate == null);
}