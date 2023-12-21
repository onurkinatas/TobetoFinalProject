using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentEducationRepository : IAsyncRepository<StudentEducation, Guid>, IRepository<StudentEducation, Guid>
{
}