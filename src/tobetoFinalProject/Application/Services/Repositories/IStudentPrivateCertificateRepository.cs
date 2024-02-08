using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentPrivateCertificateRepository : IAsyncRepository<StudentPrivateCertificate, Guid>, IRepository<StudentPrivateCertificate, Guid>
{
}