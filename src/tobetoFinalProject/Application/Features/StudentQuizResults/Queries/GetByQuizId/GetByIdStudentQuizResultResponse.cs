using Core.Application.Responses;

namespace Application.Features.StudentQuizResults.Queries.GetByQuizId;

public class GetByIdStudentQuizResultResponse : IResponse
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public int Point { get; set; }
    public int CorrectAnswerCount { get; set; }
    public int WrongAnswerCount { get; set; }
    public int EmptyAnswerCount { get; set; }
  
}