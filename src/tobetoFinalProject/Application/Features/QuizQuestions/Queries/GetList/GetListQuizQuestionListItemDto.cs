using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.QuizQuestions.Queries.GetList;

public class GetListQuizQuestionListItemDto : IDto
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
}