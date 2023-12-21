using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentAppealRepository : EfRepositoryBase<StudentAppeal, Guid, BaseDbContext>, IStudentAppealRepository
{
    public StudentAppealRepository(BaseDbContext context) : base(context)
    {
    }
}