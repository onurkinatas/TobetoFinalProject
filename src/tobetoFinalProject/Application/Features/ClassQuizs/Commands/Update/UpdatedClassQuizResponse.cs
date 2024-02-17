using Core.Application.Responses;

namespace Application.Features.ClassQuizs.Commands.Update;

public class UpdatedClassQuizResponse : IResponse
{
    public int Id { get; set; }
    public Guid StudentClassId { get; set; }
    public int QuizId { get; set; }
}