using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentSocialMediaRepository : EfRepositoryBase<StudentSocialMedia, Guid, BaseDbContext>, IStudentSocialMediaRepository
{
    public StudentSocialMediaRepository(BaseDbContext context) : base(context)
    {
    }
}