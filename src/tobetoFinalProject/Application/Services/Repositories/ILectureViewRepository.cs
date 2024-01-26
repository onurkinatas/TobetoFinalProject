using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ILectureViewRepository : IAsyncRepository<LectureView, Guid>, IRepository<LectureView, Guid>
{
}