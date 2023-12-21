using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SocialMedias;

public interface ISocialMediasService
{
    Task<SocialMedia?> GetAsync(
        Expression<Func<SocialMedia, bool>> predicate,
        Func<IQueryable<SocialMedia>, IIncludableQueryable<SocialMedia, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<SocialMedia>?> GetListAsync(
        Expression<Func<SocialMedia, bool>>? predicate = null,
        Func<IQueryable<SocialMedia>, IOrderedQueryable<SocialMedia>>? orderBy = null,
        Func<IQueryable<SocialMedia>, IIncludableQueryable<SocialMedia, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<SocialMedia> AddAsync(SocialMedia socialMedia);
    Task<SocialMedia> UpdateAsync(SocialMedia socialMedia);
    Task<SocialMedia> DeleteAsync(SocialMedia socialMedia, bool permanent = false);
}
