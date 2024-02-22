using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class LectureViewRepository : EfRepositoryBase<LectureView, Guid, BaseDbContext>, ILectureViewRepository
{
    public LectureViewRepository(BaseDbContext context) : base(context)
    {
    }
    public async Task<ICollection<LectureView>> GetAll(Expression<Func<LectureView, bool>> filter = null) => filter == null ? Context.Set<LectureView>().ToList()
            : Context.Set<LectureView>().Where(e => e.DeletedDate == null).Where(filter).ToList();
    public int GetCount(Expression<Func<LectureView, bool>> filter = null)=> 
        filter != null ? Context.Set<LectureView>().Where(filter).Count(e => e.DeletedDate == null) : Context.Set<LectureView>().Count(e => e.DeletedDate == null);

}