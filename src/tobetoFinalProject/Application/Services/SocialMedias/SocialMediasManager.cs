using Application.Features.SocialMedias.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SocialMedias;

public class SocialMediasManager : ISocialMediasService
{
    private readonly ISocialMediaRepository _socialMediaRepository;
    private readonly SocialMediaBusinessRules _socialMediaBusinessRules;

    public SocialMediasManager(ISocialMediaRepository socialMediaRepository, SocialMediaBusinessRules socialMediaBusinessRules)
    {
        _socialMediaRepository = socialMediaRepository;
        _socialMediaBusinessRules = socialMediaBusinessRules;
    }

    public async Task<SocialMedia?> GetAsync(
        Expression<Func<SocialMedia, bool>> predicate,
        Func<IQueryable<SocialMedia>, IIncludableQueryable<SocialMedia, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        SocialMedia? socialMedia = await _socialMediaRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return socialMedia;
    }

    public async Task<IPaginate<SocialMedia>?> GetListAsync(
        Expression<Func<SocialMedia, bool>>? predicate = null,
        Func<IQueryable<SocialMedia>, IOrderedQueryable<SocialMedia>>? orderBy = null,
        Func<IQueryable<SocialMedia>, IIncludableQueryable<SocialMedia, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<SocialMedia> socialMediaList = await _socialMediaRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return socialMediaList;
    }

    public async Task<SocialMedia> AddAsync(SocialMedia socialMedia)
    {
        SocialMedia addedSocialMedia = await _socialMediaRepository.AddAsync(socialMedia);

        return addedSocialMedia;
    }

    public async Task<SocialMedia> UpdateAsync(SocialMedia socialMedia)
    {
        SocialMedia updatedSocialMedia = await _socialMediaRepository.UpdateAsync(socialMedia);

        return updatedSocialMedia;
    }

    public async Task<SocialMedia> DeleteAsync(SocialMedia socialMedia, bool permanent = false)
    {
        SocialMedia deletedSocialMedia = await _socialMediaRepository.DeleteAsync(socialMedia);

        return deletedSocialMedia;
    }
}
