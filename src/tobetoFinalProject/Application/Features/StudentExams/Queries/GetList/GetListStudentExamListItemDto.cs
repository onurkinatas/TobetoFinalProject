using Core.Application.Dtos;

namespace Application.Features.StudentExams.Queries.GetList;

public class GetListStudentExamListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentId { get; set; }
}