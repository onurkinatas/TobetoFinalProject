using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ClassLectureRepository : EfRepositoryBase<ClassLecture, Guid, BaseDbContext>, IClassLectureRepository
{
    public ClassLectureRepository(BaseDbContext context) : base(context)
    {
    }
}