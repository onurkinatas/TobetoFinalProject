using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentCertificateRepository : EfRepositoryBase<StudentCertificate, Guid, BaseDbContext>, IStudentCertificateRepository
{
    public StudentCertificateRepository(BaseDbContext context) : base(context)
    {
    }
}