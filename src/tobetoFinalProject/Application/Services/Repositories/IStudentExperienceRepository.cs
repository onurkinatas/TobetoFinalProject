using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentExperienceRepository : IAsyncRepository<StudentExperience, Guid>, IRepository<StudentExperience, Guid>
{
}