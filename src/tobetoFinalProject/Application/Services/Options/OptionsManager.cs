using Application.Features.Options.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Options;

public class OptionsManager : IOptionsService
{
    private readonly IOptionRepository _optionRepository;
    private readonly OptionBusinessRules _optionBusinessRules;

    public OptionsManager(IOptionRepository optionRepository, OptionBusinessRules optionBusinessRules)
    {
        _optionRepository = optionRepository;
        _optionBusinessRules = optionBusinessRules;
    }

    public async Task<Option?> GetAsync(
        Expression<Func<Option, bool>> predicate,
        Func<IQueryable<Option>, IIncludableQueryable<Option, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Option? option = await _optionRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return option;
    }

    public async Task<IPaginate<Option>?> GetListAsync(
        Expression<Func<Option, bool>>? predicate = null,
        Func<IQueryable<Option>, IOrderedQueryable<Option>>? orderBy = null,
        Func<IQueryable<Option>, IIncludableQueryable<Option, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Option> optionList = await _optionRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return optionList;
    }

    public async Task<Option> AddAsync(Option option)
    {
        Option addedOption = await _optionRepository.AddAsync(option);

        return addedOption;
    }

    public async Task<Option> UpdateAsync(Option option)
    {
        Option updatedOption = await _optionRepository.UpdateAsync(option);

        return updatedOption;
    }

    public async Task<Option> DeleteAsync(Option option, bool permanent = false)
    {
        Option deletedOption = await _optionRepository.DeleteAsync(option);

        return deletedOption;
    }
}
