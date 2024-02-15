using Core.Application.Dtos;

namespace Application.Features.StudentQuizOptions.Queries.GetList;

public class GetListStudentQuizOptionListItemDto : IDto
{
    public int Id { get; set; }
    public Guid ExamId { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public int OptionId { get; set; }
    public Guid StudentId { get; set; }
}