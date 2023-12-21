using Core.Application.Responses;

namespace Application.Features.StudentAppeals.Commands.Update;

public class UpdatedStudentAppealResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid AppealId { get; set; }
}