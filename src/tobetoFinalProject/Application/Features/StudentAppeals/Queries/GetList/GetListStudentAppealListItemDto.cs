using Application.Features.AppealStages.Queries.GetList;
using Application.Features.Stages.Queries.GetList;
using Application.Features.StudentStages.Queries.GetList;
using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.StudentAppeals.Queries.GetList;

public class GetListStudentAppealListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid AppealId { get; set; }
    public string AppealName { get; set; }
    public DateTime AppealStartTime { get; set; }
    public DateTime AppealEndTime { get; set; }
    public ICollection<GetListStageListItemDto> Stages { get; set; }
}