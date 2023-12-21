using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class LectureRepository : EfRepositoryBase<Lecture, Guid, BaseDbContext>, ILectureRepository
{
    public LectureRepository(BaseDbContext context) : base(context)
    {
    }
}