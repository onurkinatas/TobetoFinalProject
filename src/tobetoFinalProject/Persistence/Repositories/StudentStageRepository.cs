using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentStageRepository : EfRepositoryBase<StudentStage, Guid, BaseDbContext>, IStudentStageRepository
{
    public StudentStageRepository(BaseDbContext context) : base(context)
    {
    }
}