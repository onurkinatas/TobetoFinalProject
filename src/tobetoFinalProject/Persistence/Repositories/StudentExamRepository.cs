using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentExamRepository : EfRepositoryBase<StudentExam, Guid, BaseDbContext>, IStudentExamRepository
{
    public StudentExamRepository(BaseDbContext context) : base(context)
    {
    }
}