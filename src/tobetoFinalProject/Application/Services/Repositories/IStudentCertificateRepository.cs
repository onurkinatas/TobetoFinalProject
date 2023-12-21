using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentCertificateRepository : IAsyncRepository<StudentCertificate, Guid>, IRepository<StudentCertificate, Guid>
{
}