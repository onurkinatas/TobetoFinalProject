using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentLectureCommentRepository : EfRepositoryBase<StudentLectureComment, int, BaseDbContext>, IStudentLectureCommentRepository
{
    public StudentLectureCommentRepository(BaseDbContext context) : base(context)
    {
    }
}