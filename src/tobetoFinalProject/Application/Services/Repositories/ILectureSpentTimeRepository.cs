using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ILectureSpentTimeRepository : IAsyncRepository<LectureSpentTime, Guid>, IRepository<LectureSpentTime, Guid>
{
}