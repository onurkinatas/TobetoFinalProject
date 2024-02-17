using Core.Application.Responses;

namespace Application.Features.GeneralQuizs.Queries.GetById;

public class GetByIdGeneralQuizResponse : IResponse
{
    public int Id { get; set; }
    public int QuizId { get; set; }
}