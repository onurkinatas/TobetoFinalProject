using Application.Features.Appeals.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Appeals;

public class AppealsManager : IAppealsService
{
    private readonly IAppealRepository _appealRepository;
    private readonly AppealBusinessRules _appealBusinessRules;

    public AppealsManager(IAppealRepository appealRepository, AppealBusinessRules appealBusinessRules)
    {
        _appealRepository = appealRepository;
        _appealBusinessRules = appealBusinessRules;
    }

    public async Task<Appeal?> GetAsync(
        Expression<Func<Appeal, bool>> predicate,
        Func<IQueryable<Appeal>, IIncludableQueryable<Appeal, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Appeal? appeal = await _appealRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return appeal;
    }

    public async Task<IPaginate<Appeal>?> GetListAsync(
        Expression<Func<Appeal, bool>>? predicate = null,
        Func<IQueryable<Appeal>, IOrderedQueryable<Appeal>>? orderBy = null,
        Func<IQueryable<Appeal>, IIncludableQueryable<Appeal, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Appeal> appealList = await _appealRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return appealList;
    }

    public async Task<Appeal> AddAsync(Appeal appeal)
    {
        Appeal addedAppeal = await _appealRepository.AddAsync(appeal);

        return addedAppeal;
    }

    public async Task<Appeal> UpdateAsync(Appeal appeal)
    {
        Appeal updatedAppeal = await _appealRepository.UpdateAsync(appeal);

        return updatedAppeal;
    }

    public async Task<Appeal> DeleteAsync(Appeal appeal, bool permanent = false)
    {
        Appeal deletedAppeal = await _appealRepository.DeleteAsync(appeal);

        return deletedAppeal;
    }
}
