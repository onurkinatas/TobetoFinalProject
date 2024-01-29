using Domain.Entities;
using Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface ILectureViewRepository : IAsyncRepository<LectureView, Guid>, IRepository<LectureView, Guid>
{
    public Task<ICollection<LectureView>> GetAll(Expression<Func<LectureView, bool>> filter = null);
}