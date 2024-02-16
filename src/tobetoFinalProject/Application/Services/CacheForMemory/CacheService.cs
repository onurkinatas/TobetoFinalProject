using Application.Services.ContextOperations;
using Application.Services.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services.CacheForMemory;

public class CacheService:ICacheMemoryService
{
    private readonly IMemoryCache _cache;
    private readonly IContextOperationService _contextOperationService;
    public CacheService(IMemoryCache cache, IContextOperationService contextOperationService)
    {
        _cache = cache;
        _contextOperationService = contextOperationService;
    }

    public void AddStudentClassIdFromCache(Guid studentId) => throw new NotImplementedException();

    public void AddStudentIdToCache(Guid studentId)
    {

        if (_cache.TryGetValue($"studentId", out Guid cachedStudentId))
        {
            if (cachedStudentId != null)
            {
                _cache.Remove($"studentId");
                _cache.Set($"studentId", studentId, TimeSpan.FromMinutes(30));
            }

        }
        else
        {
            _cache.Set($"studentId", studentId, TimeSpan.FromMinutes(30));
        }
    }

    public List<Guid> GetStudentClassIdFromCache() => throw new NotImplementedException();

    public Guid? GetStudentIdFromCache()
    {
        if (_cache.TryGetValue($"studentId", out Guid cachedStudentId))
        {
            return cachedStudentId;
        }
        return null;
    }

  
}