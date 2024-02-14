using Core.Application.Responses;

namespace Application.Features.Quizs.Commands.Delete;

public class DeletedQuizResponse : IResponse
{
    public int Id { get; set; }
}