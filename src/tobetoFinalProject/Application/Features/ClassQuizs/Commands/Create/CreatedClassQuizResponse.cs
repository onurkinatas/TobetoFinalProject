using Core.Application.Responses;

namespace Application.Features.ClassQuizs.Commands.Create;

public class CreatedClassQuizResponse : IResponse
{
    public int Id { get; set; }
    public Guid StudentClassId { get; set; }
    public int QuizId { get; set; }
}