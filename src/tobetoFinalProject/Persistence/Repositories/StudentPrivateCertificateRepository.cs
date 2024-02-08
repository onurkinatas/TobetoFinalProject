using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentPrivateCertificateRepository : EfRepositoryBase<StudentPrivateCertificate, Guid, BaseDbContext>, IStudentPrivateCertificateRepository
{
    public StudentPrivateCertificateRepository(BaseDbContext context) : base(context)
    {
    }
}