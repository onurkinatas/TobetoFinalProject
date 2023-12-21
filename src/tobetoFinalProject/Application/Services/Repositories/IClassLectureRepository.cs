using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IClassLectureRepository : IAsyncRepository<ClassLecture, Guid>, IRepository<ClassLecture, Guid>
{
}