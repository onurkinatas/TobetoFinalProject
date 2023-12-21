using Core.Application.Responses;

namespace Application.Features.StudentLanguageLevels.Commands.Update;

public class UpdatedStudentLanguageLevelResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LanguageLevelId { get; set; }
}