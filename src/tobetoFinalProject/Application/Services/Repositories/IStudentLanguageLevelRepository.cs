using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentLanguageLevelRepository : IAsyncRepository<StudentLanguageLevel, Guid>, IRepository<StudentLanguageLevel, Guid>
{
}