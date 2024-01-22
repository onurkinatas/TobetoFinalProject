using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ContextOperations;
public interface IContextOperationService
{
    Task<Student> GetStudentFromContext();
    Task<ICollection<Guid>> GetStudentClassesFromContext();
}

