using Core.Application.Responses;

namespace Application.Features.Quizs.Queries.GetById;

public class GetByIdQuizResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public int QuizQuestionCount { get; set; }
}