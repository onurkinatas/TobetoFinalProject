using Core.Application.Responses;

namespace Application.Features.QuizQuestions.Commands.Update;

public class UpdatedQuizQuestionResponse : IResponse
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
}