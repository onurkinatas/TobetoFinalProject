using Core.Application.Responses;

namespace Application.Features.StudentQuizResults.Commands.Create;

public class CreatedStudentQuizResultResponse : IResponse
{
    public int Id { get; set; }
    public Guid StudentId { get; set; }
    public int QuizId { get; set; }
    public int CorrectAnswerCount { get; set; }
    public int WrongAnswerCount { get; set; }
    public int EmptyAnswerCount { get; set; }
   
}