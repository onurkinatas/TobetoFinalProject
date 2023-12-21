using Core.Application.Dtos;

namespace Application.Features.StudentAppeals.Queries.GetList;

public class GetListStudentAppealListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid AppealId { get; set; }
}