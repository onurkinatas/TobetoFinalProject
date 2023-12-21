using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICourseContentRepository : IAsyncRepository<CourseContent, Guid>, IRepository<CourseContent, Guid>
{
}