using Core.Application.Dtos;

namespace Application.Features.StudentQuizResults.Queries.GetList;

public class GetListStudentQuizResultListItemDto : IDto
{
    public int Id { get; set; }
    public int QuizId { get; set; }

}