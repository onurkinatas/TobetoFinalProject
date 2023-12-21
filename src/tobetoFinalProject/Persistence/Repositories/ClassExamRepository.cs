using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ClassExamRepository : EfRepositoryBase<ClassExam, Guid, BaseDbContext>, IClassExamRepository
{
    public ClassExamRepository(BaseDbContext context) : base(context)
    {
    }
}