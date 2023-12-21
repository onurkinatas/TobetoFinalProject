using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ILectureCourseRepository : IAsyncRepository<LectureCourse, Guid>, IRepository<LectureCourse, Guid>
{
}