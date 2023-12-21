using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IManufacturerRepository : IAsyncRepository<Manufacturer, Guid>, IRepository<Manufacturer, Guid>
{
}