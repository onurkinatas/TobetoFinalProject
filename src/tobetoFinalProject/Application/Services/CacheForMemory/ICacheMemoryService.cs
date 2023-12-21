using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CacheForMemory;
public interface ICacheMemoryService
{
    public void AddStudentIdToCache(Guid studentId);
    public Guid? GetStudentIdFromCache();
    public void AddStudentClassIdFromCache(Guid studentId);
    public List<Guid> GetStudentClassIdFromCache();
}
