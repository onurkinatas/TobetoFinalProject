using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Manufacturers;

public interface IManufacturersService
{
    Task<Manufacturer?> GetAsync(
        Expression<Func<Manufacturer, bool>> predicate,
        Func<IQueryable<Manufacturer>, IIncludableQueryable<Manufacturer, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Manufacturer>?> GetListAsync(
        Expression<Func<Manufacturer, bool>>? predicate = null,
        Func<IQueryable<Manufacturer>, IOrderedQueryable<Manufacturer>>? orderBy = null,
        Func<IQueryable<Manufacturer>, IIncludableQueryable<Manufacturer, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Manufacturer> AddAsync(Manufacturer manufacturer);
    Task<Manufacturer> UpdateAsync(Manufacturer manufacturer);
    Task<Manufacturer> DeleteAsync(Manufacturer manufacturer, bool permanent = false);
}
