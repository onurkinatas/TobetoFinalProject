using Core.Application.Responses;

namespace Application.Features.QuizQuestions.Queries.GetById;

public class GetByIdQuizQuestionResponse : IResponse
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
}