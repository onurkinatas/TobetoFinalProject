using Core.Application.Responses;

namespace Application.Features.StudentAppeals.Queries.GetById;

public class GetByIdStudentAppealResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid AppealId { get; set; }
}