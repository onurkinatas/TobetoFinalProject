using Application.Services.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services.CacheForMemory;

public class CacheService : ICacheMemoryService
{
    private readonly IMemoryCache _cache;
    private readonly IStudentClassStudentRepository _studentStudentClassRepository;

    public CacheService(IMemoryCache cache, IStudentClassStudentRepository studentStudentClassRepository)
    {
        _cache = cache;
        _studentStudentClassRepository = studentStudentClassRepository;
    }

    public void AddStudentIdToCache(Guid studentId)
    {

        if (_cache.TryGetValue($"studentId", out Guid cachedStudentId))
        {
            if (cachedStudentId != null)
            {
                _cache.Remove($"studentId");
                _cache.Set($"studentId", studentId, TimeSpan.FromMinutes(30));
                AddStudentClassIdFromCache(studentId);
            }

        }
        else
        {
            _cache.Set($"studentId", studentId, TimeSpan.FromMinutes(30));
            AddStudentClassIdFromCache(studentId);
        }
    }

    public Guid? GetStudentIdFromCache()
    {
        if (_cache.TryGetValue($"studentId", out Guid cachedStudentId))
        {
            return cachedStudentId;
        }
        return null;
    }

    public void AddStudentClassIdFromCache(Guid studentId)
    {
        var classIds = _studentStudentClassRepository
            .GetAllWithoutPaginate(sc => sc.StudentId == studentId)
            .Select(sc => sc.StudentClassId)
            .ToList();
        if (_cache.TryGetValue($"studentClassIds", out List<Guid> cachedStudentClassId))
        {
            if (cachedStudentClassId != null)
            {
                _cache.Remove($"studentClassId");
                _cache.Set($"studentClassId", classIds, TimeSpan.FromMinutes(30));
            }
        }
        else
        {
            _cache.Set($"studentClassId", classIds, TimeSpan.FromMinutes(30));
        }
    }

    public List<Guid>? GetStudentClassIdFromCache()
    {
        if (_cache.TryGetValue($"studentClassId", out List<Guid> cachedStudentClassId))
        {
            return cachedStudentClassId;
        }
        return null;
    }
}