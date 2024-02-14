using Core.Application.Responses;

namespace Application.Features.QuizQuestions.Commands.Create;

public class CreatedQuizQuestionResponse : IResponse
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
}