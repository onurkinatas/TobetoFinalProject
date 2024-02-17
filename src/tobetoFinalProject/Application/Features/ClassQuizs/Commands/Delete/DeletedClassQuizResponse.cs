using Core.Application.Responses;

namespace Application.Features.ClassQuizs.Commands.Delete;

public class DeletedClassQuizResponse : IResponse
{
    public int Id { get; set; }
}