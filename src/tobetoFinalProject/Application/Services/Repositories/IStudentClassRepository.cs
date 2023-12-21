using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentClassRepository : IAsyncRepository<StudentClass, Guid>, IRepository<StudentClass, Guid>
{
}