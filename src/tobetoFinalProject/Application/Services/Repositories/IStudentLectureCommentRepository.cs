using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentLectureCommentRepository : IAsyncRepository<StudentLectureComment, int>, IRepository<StudentLectureComment, int>
{
}