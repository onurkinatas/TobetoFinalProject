using Domain.Entities;
using Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface IStudentClassStudentRepository : IAsyncRepository<StudentClassStudent, Guid>, IRepository<StudentClassStudent, Guid>
{
    public ICollection<StudentClassStudent> GetAllWithoutPaginate(Expression<Func<StudentClassStudent, bool>> filter = null);
}