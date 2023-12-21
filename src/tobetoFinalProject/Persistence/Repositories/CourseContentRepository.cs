using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CourseContentRepository : EfRepositoryBase<CourseContent, Guid, BaseDbContext>, ICourseContentRepository
{
    public CourseContentRepository(BaseDbContext context) : base(context)
    {
    }
}