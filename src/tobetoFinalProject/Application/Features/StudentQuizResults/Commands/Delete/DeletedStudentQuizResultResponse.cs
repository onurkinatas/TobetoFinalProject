using Core.Application.Responses;

namespace Application.Features.StudentQuizResults.Commands.Delete;

public class DeletedStudentQuizResultResponse : IResponse
{
    public int Id { get; set; }
}