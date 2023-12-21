using Core.Application.Responses;

namespace Application.Features.StudentLanguageLevels.Queries.GetById;

public class GetByIdStudentLanguageLevelResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LanguageLevelId { get; set; }
}