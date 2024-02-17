using Core.Application.Responses;

namespace Application.Features.GeneralQuizs.Commands.Create;

public class CreatedGeneralQuizResponse : IResponse
{
    public int Id { get; set; }
    public int QuizId { get; set; }
}