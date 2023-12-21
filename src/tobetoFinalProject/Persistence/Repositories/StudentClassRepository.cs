using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentClassRepository : EfRepositoryBase<StudentClass, Guid, BaseDbContext>, IStudentClassRepository
{
    public StudentClassRepository(BaseDbContext context) : base(context)
    {
    }
}