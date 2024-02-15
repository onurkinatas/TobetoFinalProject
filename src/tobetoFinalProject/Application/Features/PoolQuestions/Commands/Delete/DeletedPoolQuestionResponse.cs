using Core.Application.Responses;

namespace Application.Features.PoolQuestions.Commands.Delete;

public class DeletedPoolQuestionResponse : IResponse
{
    public int Id { get; set; }
}