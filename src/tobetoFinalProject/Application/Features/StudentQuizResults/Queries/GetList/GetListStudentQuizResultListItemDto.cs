using Core.Application.Dtos;

namespace Application.Features.StudentQuizResults.Queries.GetList;

public class GetListStudentQuizResultListItemDto : IDto
{
    public int Id { get; set; }
    public Guid StudentId { get; set; }
    public int QuizId { get; set; }
    public int CorrectAnswerCount { get; set; }
    public int WrongAnswerCount { get; set; }
    public int EmptyAnswerCount { get; set; }

}