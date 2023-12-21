using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ILectureRepository : IAsyncRepository<Lecture, Guid>, IRepository<Lecture, Guid>
{
}