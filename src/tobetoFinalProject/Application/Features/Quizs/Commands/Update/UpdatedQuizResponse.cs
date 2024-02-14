using Core.Application.Responses;

namespace Application.Features.Quizs.Commands.Update;

public class UpdatedQuizResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public Guid ExamId { get; set; }
}