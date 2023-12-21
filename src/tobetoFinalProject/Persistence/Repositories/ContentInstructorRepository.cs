using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContentInstructorRepository : EfRepositoryBase<ContentInstructor, Guid, BaseDbContext>, IContentInstructorRepository
{
    public ContentInstructorRepository(BaseDbContext context) : base(context)
    {
    }
}