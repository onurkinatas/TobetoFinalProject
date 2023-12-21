using Core.Application.Responses;

namespace Application.Features.Stages.Commands.Delete;

public class DeletedStageResponse : IResponse
{
    public Guid Id { get; set; }
}