using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class LectureViewRepository : EfRepositoryBase<LectureView, Guid, BaseDbContext>, ILectureViewRepository
{
    public LectureViewRepository(BaseDbContext context) : base(context)
    {
    }
}