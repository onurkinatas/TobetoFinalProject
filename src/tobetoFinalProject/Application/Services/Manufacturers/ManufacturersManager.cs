using Application.Features.Manufacturers.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Manufacturers;

public class ManufacturersManager : IManufacturersService
{
    private readonly IManufacturerRepository _manufacturerRepository;
    private readonly ManufacturerBusinessRules _manufacturerBusinessRules;

    public ManufacturersManager(IManufacturerRepository manufacturerRepository, ManufacturerBusinessRules manufacturerBusinessRules)
    {
        _manufacturerRepository = manufacturerRepository;
        _manufacturerBusinessRules = manufacturerBusinessRules;
    }

    public async Task<Manufacturer?> GetAsync(
        Expression<Func<Manufacturer, bool>> predicate,
        Func<IQueryable<Manufacturer>, IIncludableQueryable<Manufacturer, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Manufacturer? manufacturer = await _manufacturerRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return manufacturer;
    }

    public async Task<IPaginate<Manufacturer>?> GetListAsync(
        Expression<Func<Manufacturer, bool>>? predicate = null,
        Func<IQueryable<Manufacturer>, IOrderedQueryable<Manufacturer>>? orderBy = null,
        Func<IQueryable<Manufacturer>, IIncludableQueryable<Manufacturer, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Manufacturer> manufacturerList = await _manufacturerRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return manufacturerList;
    }

    public async Task<Manufacturer> AddAsync(Manufacturer manufacturer)
    {
        Manufacturer addedManufacturer = await _manufacturerRepository.AddAsync(manufacturer);

        return addedManufacturer;
    }

    public async Task<Manufacturer> UpdateAsync(Manufacturer manufacturer)
    {
        Manufacturer updatedManufacturer = await _manufacturerRepository.UpdateAsync(manufacturer);

        return updatedManufacturer;
    }

    public async Task<Manufacturer> DeleteAsync(Manufacturer manufacturer, bool permanent = false)
    {
        Manufacturer deletedManufacturer = await _manufacturerRepository.DeleteAsync(manufacturer);

        return deletedManufacturer;
    }
}
