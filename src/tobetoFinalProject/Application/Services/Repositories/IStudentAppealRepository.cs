using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentAppealRepository : IAsyncRepository<StudentAppeal, Guid>, IRepository<StudentAppeal, Guid>
{
}