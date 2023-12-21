using Core.Application.Dtos;

namespace Application.Features.ClassExams.Queries.GetList;

public class GetListClassExamListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentClassId { get; set; }
}