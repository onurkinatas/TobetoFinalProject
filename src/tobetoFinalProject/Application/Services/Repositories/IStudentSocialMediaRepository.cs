using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentSocialMediaRepository : IAsyncRepository<StudentSocialMedia, Guid>, IRepository<StudentSocialMedia, Guid>
{
}