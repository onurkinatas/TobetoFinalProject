using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentClassStudentRepository : EfRepositoryBase<StudentClassStudent, Guid, BaseDbContext>, IStudentClassStudentRepository
{
    public StudentClassStudentRepository(BaseDbContext context) : base(context)
    {
    }
}