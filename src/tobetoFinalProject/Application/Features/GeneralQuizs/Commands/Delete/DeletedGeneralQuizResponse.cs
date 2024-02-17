using Core.Application.Responses;

namespace Application.Features.GeneralQuizs.Commands.Delete;

public class DeletedGeneralQuizResponse : IResponse
{
    public int Id { get; set; }
}