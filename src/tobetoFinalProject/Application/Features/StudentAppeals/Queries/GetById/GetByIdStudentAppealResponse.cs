using Application.Features.StudentStages.Queries.GetById;
using Application.Features.StudentStages.Queries.GetList;
using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.StudentAppeals.Queries.GetById;

public class GetByIdStudentAppealResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid AppealId { get; set; }
    public string AppealName { get; set; }
    public DateTime AppealStartTime { get; set; }
    public DateTime AppealEndTime { get; set; }
}