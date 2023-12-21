using Core.Application.Responses;

namespace Application.Features.StudentAppeals.Commands.Delete;

public class DeletedStudentAppealResponse : IResponse
{
    public Guid Id { get; set; }
}