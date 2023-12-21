using Application.Features.SubTypes.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SubTypes;

public class SubTypesManager : ISubTypesService
{
    private readonly ISubTypeRepository _subTypeRepository;
    private readonly SubTypeBusinessRules _subTypeBusinessRules;

    public SubTypesManager(ISubTypeRepository subTypeRepository, SubTypeBusinessRules subTypeBusinessRules)
    {
        _subTypeRepository = subTypeRepository;
        _subTypeBusinessRules = subTypeBusinessRules;
    }

    public async Task<SubType?> GetAsync(
        Expression<Func<SubType, bool>> predicate,
        Func<IQueryable<SubType>, IIncludableQueryable<SubType, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        SubType? subType = await _subTypeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return subType;
    }

    public async Task<IPaginate<SubType>?> GetListAsync(
        Expression<Func<SubType, bool>>? predicate = null,
        Func<IQueryable<SubType>, IOrderedQueryable<SubType>>? orderBy = null,
        Func<IQueryable<SubType>, IIncludableQueryable<SubType, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<SubType> subTypeList = await _subTypeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return subTypeList;
    }

    public async Task<SubType> AddAsync(SubType subType)
    {
        SubType addedSubType = await _subTypeRepository.AddAsync(subType);

        return addedSubType;
    }

    public async Task<SubType> UpdateAsync(SubType subType)
    {
        SubType updatedSubType = await _subTypeRepository.UpdateAsync(subType);

        return updatedSubType;
    }

    public async Task<SubType> DeleteAsync(SubType subType, bool permanent = false)
    {
        SubType deletedSubType = await _subTypeRepository.DeleteAsync(subType);

        return deletedSubType;
    }
}
