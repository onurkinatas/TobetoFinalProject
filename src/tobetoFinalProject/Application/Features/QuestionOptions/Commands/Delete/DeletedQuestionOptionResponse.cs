using Core.Application.Responses;

namespace Application.Features.QuestionOptions.Commands.Delete;

public class DeletedQuestionOptionResponse : IResponse
{
    public int Id { get; set; }
}