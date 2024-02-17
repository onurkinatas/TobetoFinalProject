using Core.Application.Responses;

namespace Application.Features.GeneralQuizs.Commands.Update;

public class UpdatedGeneralQuizResponse : IResponse
{
    public int Id { get; set; }
    public int QuizId { get; set; }
}