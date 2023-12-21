using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ILectureLikeRepository : IAsyncRepository<LectureLike, Guid>, IRepository<LectureLike, Guid>
{
}