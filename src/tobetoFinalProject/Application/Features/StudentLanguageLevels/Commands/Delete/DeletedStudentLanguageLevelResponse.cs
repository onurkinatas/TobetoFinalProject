using Core.Application.Responses;

namespace Application.Features.StudentLanguageLevels.Commands.Delete;

public class DeletedStudentLanguageLevelResponse : IResponse
{
    public Guid Id { get; set; }
}