using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentEducationRepository : EfRepositoryBase<StudentEducation, Guid, BaseDbContext>, IStudentEducationRepository
{
    public StudentEducationRepository(BaseDbContext context) : base(context)
    {
    }
}