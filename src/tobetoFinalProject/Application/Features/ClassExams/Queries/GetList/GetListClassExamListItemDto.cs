using Core.Application.Dtos;

namespace Application.Features.ClassExams.Queries.GetList;

public class GetListClassExamListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentClassId { get; set; }
    public string ExamName { get; set; }
    public bool ExamIsActive { get; set; }
    public string ExamExamUrl { get; set; }
    public string StudentClassName { get; set; }
}