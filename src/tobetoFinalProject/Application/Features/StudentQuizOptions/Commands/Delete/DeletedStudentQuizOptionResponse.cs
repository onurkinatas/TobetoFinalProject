using Core.Application.Responses;

namespace Application.Features.StudentQuizOptions.Commands.Delete;

public class DeletedStudentQuizOptionResponse : IResponse
{
    public int Id { get; set; }
}