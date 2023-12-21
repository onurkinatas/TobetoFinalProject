using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ManufacturerRepository : EfRepositoryBase<Manufacturer, Guid, BaseDbContext>, IManufacturerRepository
{
    public ManufacturerRepository(BaseDbContext context) : base(context)
    {
    }
}