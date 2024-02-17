using Core.Application.Responses;

namespace Application.Features.ClassQuizs.Queries.GetById;

public class GetByIdClassQuizResponse : IResponse
{
    public int Id { get; set; }
    public Guid StudentClassId { get; set; }
    public int QuizId { get; set; }
}