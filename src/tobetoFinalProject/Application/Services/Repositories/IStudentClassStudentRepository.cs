using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentClassStudentRepository : IAsyncRepository<StudentClassStudent, Guid>, IRepository<StudentClassStudent, Guid>
{
}