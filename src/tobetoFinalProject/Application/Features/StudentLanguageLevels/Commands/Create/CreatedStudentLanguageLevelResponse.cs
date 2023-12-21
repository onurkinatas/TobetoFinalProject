using Core.Application.Responses;

namespace Application.Features.StudentLanguageLevels.Commands.Create;

public class CreatedStudentLanguageLevelResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LanguageLevelId { get; set; }
}