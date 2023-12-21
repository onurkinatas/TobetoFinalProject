using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class StudentClassStudentRepository : EfRepositoryBase<StudentClassStudent, Guid, BaseDbContext>, IStudentClassStudentRepository
{
    public StudentClassStudentRepository(BaseDbContext context) : base(context)
    {
    }

    public ICollection<StudentClassStudent> GetAllWithoutPaginate(Expression<Func<StudentClassStudent, bool>> filter = null) 
    {
        return filter == null ? Context.Set<StudentClassStudent>().ToList()
            : Context.Set<StudentClassStudent>().Where(e => e.DeletedDate == null).Where(filter).ToList();
    }
}