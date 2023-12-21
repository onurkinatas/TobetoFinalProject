using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentExperienceRepository : EfRepositoryBase<StudentExperience, Guid, BaseDbContext>, IStudentExperienceRepository
{
    public StudentExperienceRepository(BaseDbContext context) : base(context)
    {
    }
}