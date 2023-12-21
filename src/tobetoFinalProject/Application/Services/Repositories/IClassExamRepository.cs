using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IClassExamRepository : IAsyncRepository<ClassExam, Guid>, IRepository<ClassExam, Guid>
{
}