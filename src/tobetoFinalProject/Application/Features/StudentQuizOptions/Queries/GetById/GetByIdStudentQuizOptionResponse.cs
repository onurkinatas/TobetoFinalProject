using Core.Application.Responses;

namespace Application.Features.StudentQuizOptions.Queries.GetById;

public class GetByIdStudentQuizOptionResponse : IResponse
{
    public int Id { get; set; }
    public Guid ExamId { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public int OptionId { get; set; }
    public Guid StudentId { get; set; }
}