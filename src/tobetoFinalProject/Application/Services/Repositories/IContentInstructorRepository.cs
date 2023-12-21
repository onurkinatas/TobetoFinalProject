using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentInstructorRepository : IAsyncRepository<ContentInstructor, Guid>, IRepository<ContentInstructor, Guid>
{
}