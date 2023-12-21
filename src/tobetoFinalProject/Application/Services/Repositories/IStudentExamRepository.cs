using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentExamRepository : IAsyncRepository<StudentExam, Guid>, IRepository<StudentExam, Guid>
{
}