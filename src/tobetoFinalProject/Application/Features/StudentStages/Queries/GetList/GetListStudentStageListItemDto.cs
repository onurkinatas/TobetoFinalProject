using Core.Application.Dtos;

namespace Application.Features.StudentStages.Queries.GetList;

public class GetListStudentStageListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StageId { get; set; }
    public Guid StudentId { get; set; }
}