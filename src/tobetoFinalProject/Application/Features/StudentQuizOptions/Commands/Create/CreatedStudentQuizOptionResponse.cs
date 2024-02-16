using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.StudentQuizOptions.Commands.Create;

public class CreatedStudentQuizOptionResponse : IResponse
{
    public Guid ExamId { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public int OptionId { get; set; }
    public Guid? StudentId { get; set; }
}